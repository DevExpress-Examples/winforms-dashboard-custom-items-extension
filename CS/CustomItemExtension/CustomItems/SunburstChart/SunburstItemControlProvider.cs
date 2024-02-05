using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.DashboardCommon.ViewerData;
using DevExpress.DashboardWin;
using DevExpress.Skins;
using DevExpress.TreeMap;
using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeMap;

namespace DevExpress.DashboardWin.CustomItemExtension {
    public class SunburstItemControlProvider : CustomControlProviderBase {
        bool skipSelectionEvent = false;
        SunburstControl sunburst;
        SunburstFlatDataAdapter dataAdapter;
        CustomDashboardItem<SunburstItemMetadata> dashboardItem;
        ToolTipController toolTipController;
        MultiDimensionalData multiDimensionalData;
        DashboardFlatDataSource flatData;
        Title emptyTitle;

        protected override Control Control { get { return sunburst; } }
        public SunburstItemControlProvider(CustomDashboardItem<SunburstItemMetadata> dashboardItem) {
            this.dashboardItem = dashboardItem;
            sunburst = new DashboardSunburstControl();
            sunburst.BorderOptions.Visible = false;
            this.dataAdapter = new SunburstFlatDataAdapter();
            sunburst.DataAdapter = dataAdapter;
            toolTipController = new ToolTipController();
            toolTipController.BeforeShow += ToolTipController_BeforeShow;
            sunburst.ToolTipController = toolTipController;
            sunburst.SelectionChanged += Sunburst_SelectionChanged;
            emptyTitle = new Title() { Visible = false };
            sunburst.Titles.Add(emptyTitle);
            sunburst.MouseClick += Sunburst_MouseClick;
            sunburst.LookAndFeel.StyleChanged += SunburstLookAndFeelStyleChanged;
            UpdateSunburstItemStyle();
        }
        protected override void UpdateControl(CustomItemData customItemData) {
            ClearDataBindings();
            if(ValidateBindings()) {
                flatData = customItemData.GetFlatData(new DashboardFlatDataSourceOptions() { AddColoringColumns = true, AddDisplayTextColumns = true });
                multiDimensionalData = customItemData.GetMultiDimensionalData();
                SetDataBindings(flatData);
                SetColorizer(flatData);
                SetSelectionMode();
            }
        }
        protected override void SetSelection(CustomItemSelection selection) {
            skipSelectionEvent = true;
            IList<DashboardFlatDataSourceRow> selectedRows = selection.GetDashboardFlatDataSourceRows(flatData);
            sunburst.SelectedItems.Clear();
            selectedRows.ForEach(r => sunburst.SelectedItems.Add(r));
            skipSelectionEvent = false;
        }
        protected override XRControl GetPrintableControl(CustomItemData customItemData, CustomItemExportInfo exportInfo) {
            PrintableComponentContainer container = new PrintableComponentContainer();
            container.PrintableComponent = sunburst;
            return container;
        }
        void ClearDataBindings() {
            sunburst.Colorizer = null;
            dataAdapter.DataSource = dataAdapter.ValueDataMember = dataAdapter.LabelDataMember = null;
            dataAdapter.GroupDataMembers.Clear();
        }
        bool ValidateBindings() {
            if(Interactivity.IsDrillDownEnabled) {
                emptyTitle.Text = "The Sunburst Item does not support Drill-Down";
                emptyTitle.Visible = true;
                return false;
            } else
                emptyTitle.Visible = false;
            return dashboardItem.Metadata.Value != null && dashboardItem.Metadata.Arguments.Count() > 0;
        }
        void SetDataBindings(DashboardFlatDataSource flatDataSource) {
            dataAdapter.ValueDataMember = dashboardItem.Metadata.Value.UniqueId;
            dataAdapter.LabelDataMember = flatDataSource.GetDisplayTextColumn(dashboardItem.Metadata.Arguments.Last().UniqueId).Name;
            dataAdapter.GroupDataMembers.AddRange(
                dashboardItem.Metadata.Arguments.Where(d => d != dashboardItem.Metadata.Arguments.Last())
                .Select(d => flatDataSource.GetDisplayTextColumn(d.UniqueId).Name).ToList());
            try {
                dataAdapter.DataSource = flatDataSource;
            } catch {
                dataAdapter.DataSource = null;
            }
        }
        void SetColorizer(DashboardFlatDataSource flatDataSource) {
            var coloringIndices = dashboardItem.Metadata.Arguments.Where(d => d.ColoringMode == ColoringMode.Hue)
                .Select(d => dashboardItem.Metadata.Arguments.IndexOf(d));
            int maxcoloringIndex = 0;
            if(coloringIndices.Any()) maxcoloringIndex = coloringIndices.Max();
            sunburst.Colorizer = new SunburstItemColorizer(flatDataSource, maxcoloringIndex);
        }
        void SetSelectionMode() {
            switch(Interactivity.MasterFilterMode) {
                case DashboardItemMasterFilterMode.None:
                    sunburst.SelectionMode = ElementSelectionMode.None;
                    return;
                case DashboardItemMasterFilterMode.Multiple:
                    sunburst.SelectionMode = ElementSelectionMode.Extended;
                    break;
                case DashboardItemMasterFilterMode.Single:
                    sunburst.SelectionMode = ElementSelectionMode.None;
                    break;
            }
        }
        void Sunburst_MouseClick(object sender, MouseEventArgs e) {
            if(Interactivity.MasterFilterMode == DashboardItemMasterFilterMode.Single) {
                SunburstHitInfo hi = sunburst.CalcHitInfo(e.Location);
                if(hi.InSunburstItem && !hi.SunburstItem.IsGroup) {
                    if(Interactivity.CanSetMasterFilter)
                        Interactivity.SetMasterFilter(hi.SunburstItem.Tag as DashboardFlatDataSourceRow);
                }
            }
        }
        void Sunburst_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(skipSelectionEvent) return;
            IEnumerable<DashboardFlatDataSourceRow> selectedRows = e.SelectedItems.OfType<DashboardFlatDataSourceRow>();
            if(selectedRows.Count() == 0 && Interactivity.CanClearMasterFilter)
                Interactivity.ClearMasterFilter();
            else if(Interactivity.CanSetMasterFilter)
                Interactivity.SetMasterFilter(selectedRows);
        }
        void ToolTipController_BeforeShow(object sender, ToolTipControllerShowEventArgs e) {
            IHierarchicalItem item = e.SelectedObject as IHierarchicalItem;
            if(item != null)
                e.ToolTip = string.Format("{0}: {1}", item.Label, multiDimensionalData.GetMeasures()[0].Format(item.Value));
        }
        void SunburstLookAndFeelStyleChanged(object sender, EventArgs e) {
            UpdateSunburstItemStyle();
        }
        void UpdateSunburstItemStyle() {
            Skin currentSkin = ChartSkins.GetSkin(sunburst.LookAndFeel);
            SkinElement element = currentSkin[ChartSkins.SkinBackground];
            sunburst.ItemStyle.BorderColor = currentSkin.TranslateColor(element.Color.BackColor);
        }
    }
    class SunburstItemColorizer : ISunburstColorizer {
        public event ColorizerChangedEventHandler ColorizerChanged;
        Color defaultColor = Color.Gray;
        readonly DashboardFlatDataSource flatData;
        int maxcoloringIndex;
        public SunburstItemColorizer(DashboardFlatDataSource flatData, int maxcoloringIndex) {
            this.flatData = flatData;
            this.maxcoloringIndex = maxcoloringIndex;
        }
        public Color GetItemColor(ISunburstItem item, SunburstItemGroupInfo group) {
            if(group.GroupLevel < maxcoloringIndex) {
                int alpha = 255 * (group.MaxGroupLevel - group.GroupLevel + 1) / (group.MaxGroupLevel + 1);
                return Color.FromArgb(alpha, defaultColor);
            }
            if(item.Tag is DashboardFlatDataSourceRow) {
                DashboardFlatDataSourceRow row = item.Tag as DashboardFlatDataSourceRow;
                int colorData = (int)flatData.GetValue(flatData.GetColoringColumn().Name, row);
                return Color.FromArgb(colorData);
            }
            if(item.Tag is List<object>) {
                IEnumerable<int> colors = (item.Tag as List<object>).OfType<DashboardFlatDataSourceRow>()
                    .Select(row => flatData.GetValue(flatData.GetColoringColumn().Name, row)).Cast<int>().Distinct();
                if(colors.Count() == 1)
                    return Color.FromArgb(colors.First());
            }
            return defaultColor;
        }
    }
    public class DashboardSunburstControl : SunburstControl {
        protected override void OnMouseClick(MouseEventArgs e) {
            if(e.Button != MouseButtons.Right)
                base.OnMouseClick(e);
        }
    }
}
