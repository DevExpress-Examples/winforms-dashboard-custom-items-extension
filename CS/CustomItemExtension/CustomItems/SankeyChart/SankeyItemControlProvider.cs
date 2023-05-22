using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.DashboardCommon.ViewerData;
using DevExpress.DashboardWin;
using DevExpress.DataProcessing;
using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.XtraCharts.Sankey;
using DevExpress.XtraReports.UI;

namespace DevExpresss.DashboardWin.CustomItemExtension {
    public class SankeyItemControlProvider : CustomControlProviderBase {
        DashboardFlatDataSource flatData;
        MultiDimensionalData multiDimensionalData;
        SankeyDiagramControl sankey;
        CustomDashboardItem<SankeyItemMetadata> dashboardItem;
        ToolTipController toolTipController;

        protected override Control Control { get { return sankey; } }

        public SankeyItemControlProvider(CustomDashboardItem<SankeyItemMetadata> dashboardItem) {
            this.dashboardItem = dashboardItem;
            sankey = new DashboardSankeyDiagramControl();
            sankey.BorderOptions.Thickness = 0;
            sankey.EmptySankeyText.Text = string.Empty;
            sankey.SelectedItemsChanged += Sankey_SelectedItemsChanged;
            toolTipController = new ToolTipController();
            toolTipController.BeforeShow += ToolTipController_BeforeShow;
            sankey.ToolTipController = toolTipController;
            sankey.SelectedItemsChanging += Sankey_SelectedItemsChanging;
            sankey.HighlightedItemsChanged += Sankey_HighlightedItemsChanged;
        }
        protected override void UpdateControl(CustomItemData customItemData) {
            multiDimensionalData = customItemData.GetMultiDimensionalData();
            flatData = customItemData.GetFlatData(new DashboardFlatDataSourceOptions() { AddColoringColumns = true, AddDisplayTextColumns = true });
            ClearBindings();
            if(ValidateBindings()) {
                SetDataBindings(flatData);
                SetSelectionMode();
            }
        }
        protected override void SetSelection(CustomItemSelection selection) {
            IList<DashboardFlatDataSourceRow> selectedRows = selection.GetDashboardFlatDataSourceRows(flatData);
            sankey.SelectedItems.Clear();
            selectedRows.ForEach(r => sankey.SelectedItems.Add(r));
        }
        protected override XRControl GetPrintableControl(CustomItemData customItemData, CustomItemExportInfo exportInfo) {
            PrintableComponentContainer container = new PrintableComponentContainer();
            container.PrintableComponent = sankey;
            return container;
        }
        void Sankey_HighlightedItemsChanged(object sender, SankeyHighlightedItemsChangedEventArgs e) {
            if((sankey.SelectionMode == SankeySelectionMode.Single && e.HighlightedNodes.Count > 0) || e.HighlightedLinks.Any(x => HasSpecialValues(x)))
                sankey.HighlightedItems.Clear();
        }
        void Sankey_SelectedItemsChanging(object sender, SankeySelectedItemsChangingEventArgs e) {
            if(e.Action == DevExpress.XtraCharts.SelectedItemsChangedAction.Reset) {
                e.Cancel = true;
                return;
            }
            if((sankey.SelectionMode == SankeySelectionMode.Single && e.NewNodes.Count > 0) || e.NewLinks.Any(x => HasSpecialValues(x)))
                e.Cancel = true;
        }
        void ToolTipController_BeforeShow(object sender, DevExpress.Utils.ToolTipControllerShowEventArgs e) {
            if(dashboardItem.Metadata.Weight == null)
                e.ToolTip = string.Empty;
            else if(e.SelectedObject is SankeyLink) {
                SankeyLink link = e.SelectedObject as SankeyLink;
                e.ToolTip = multiDimensionalData.GetMeasures()[0].Format(link.TotalWeight);
            } else if(e.SelectedObject is SankeyNode) {
                SankeyNode node = e.SelectedObject as SankeyNode;
                e.ToolTip = multiDimensionalData.GetMeasures()[0].Format(node.TotalWeight);
            }
        }
        void Sankey_SelectedItemsChanged(object sender, SankeySelectedItemsChangedEventArgs e) {
            if(sankey.SelectedItems.Count == 0 && Interactivity.CanClearMasterFilter)
                Interactivity.ClearMasterFilter();
            else if(sankey.SelectedItems.Count > 0 && Interactivity.CanSetMasterFilter)
                Interactivity.SetMasterFilter(sankey.SelectedItems.OfType<DashboardFlatDataSourceRow>());
        }
        bool ValidateBindings() {
            if(Interactivity.IsDrillDownEnabled) {
                sankey.EmptySankeyText.Text = "The Sankey Item does not support Drill-Down";
                return false;
            }
            if(dashboardItem.Metadata.Source == null || dashboardItem.Metadata.Target == null) {
                sankey.EmptySankeyText.Text = "Add the Source and Target dimensions";
                return false;
            }
            if(dashboardItem.Metadata.Source.GetDefinition().Equals(dashboardItem.Metadata.Target.GetDefinition())) {
                sankey.EmptySankeyText.Text = "Add different data fields to the Source and Target sections";
                return false;
            }
            return true;
        }
        void SetDataBindings(DashboardFlatDataSource flatData) {
            sankey.Colorizer = new SankeyItemColorizer(flatData);
            sankey.SourceDataMember = flatData.GetDisplayTextColumn(dashboardItem.Metadata.Source.UniqueId).Name;
            sankey.TargetDataMember = flatData.GetDisplayTextColumn(dashboardItem.Metadata.Target.UniqueId).Name;
            if(dashboardItem.Metadata.Weight != null)
                sankey.WeightDataMember = dashboardItem.Metadata.Weight.UniqueId;
            try {
                sankey.DataSource = flatData;
            } catch {
                sankey.DataSource = null;
            }
        }
        void ClearBindings() {
            sankey.DataSource = null;
            sankey.Colorizer = null;
            sankey.SourceDataMember = null;
            sankey.TargetDataMember = null;
            sankey.WeightDataMember = null;
        }
        void SetSelectionMode() {
            switch(Interactivity.MasterFilterMode) {
                case DashboardItemMasterFilterMode.None:
                    sankey.SelectionMode = SankeySelectionMode.None;
                    return;
                case DashboardItemMasterFilterMode.Multiple:
                    sankey.SelectionMode = SankeySelectionMode.Extended;
                    break;
                case DashboardItemMasterFilterMode.Single:
                    sankey.SelectionMode = SankeySelectionMode.Single;
                    break;
            }
        }
        bool HasSpecialValues(SankeyLink link) {
            DashboardFlatDataSourceRow row = (DashboardFlatDataSourceRow)link.Tags[0];
            return SpecialValues.IsOthersValue(flatData.GetValue(dashboardItem.Metadata.Source.UniqueId, row))
                || SpecialValues.IsOthersValue(flatData.GetValue(dashboardItem.Metadata.Target.UniqueId, row));
        }
    }
    class SankeyItemColorizer : ISankeyColorizer {
        readonly Color nodeDefaultColor = Color.FromArgb(255, 95, 139, 149);
        readonly DashboardFlatDataSource flatData;

        public SankeyItemColorizer(DashboardFlatDataSource flatData) {
            this.flatData = flatData;
        }
        public Color GetLinkSourceColor(SankeyLink link) {
            return GetLinkColorBase(link);
        }
        public Color GetLinkTargetColor(SankeyLink link) {
            return GetLinkColorBase(link);
        }
        public Color GetLinkColorBase(SankeyLink link) {
            DashboardFlatDataSourceRow row = link.Tags[0] as DashboardFlatDataSourceRow;
            int colorData = (int)flatData.GetValue(flatData.GetColoringColumn().Name, row);
            return Color.FromArgb(colorData);
        }
        public Color GetNodeColor(SankeyNode info) {
            return nodeDefaultColor;
        }
    }
    public class DashboardSankeyDiagramControl : SankeyDiagramControl {
        protected override void OnMouseUp(MouseEventArgs e) {
            if(e.Button != MouseButtons.Right)
                base.OnMouseUp(e);
        }
    }
}
