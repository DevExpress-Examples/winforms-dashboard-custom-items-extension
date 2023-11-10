using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using DevExpress.DashboardWin.Native;
using DevExpress.Utils.Svg;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpresss.DashboardWin.CustomItemExtension.Heatmap;
using System;
using System.IO;
using System.Reflection;

namespace DevExpresss.DashboardWin.CustomItemExtension {
    public class HeatmapItemExtensionModule : IExtensionModule {
        BarButtonItem editColorsBarItem;
        BarCheckItem barShowLabelsItem;
        BarCheckItem barShowLegendItem;
        BarCheckItem barEnableZoomXAxisItem;
        BarCheckItem barEnableZoomYAxisItem;
        CustomDashboardItem<HeatmapItemMetadata> SelectedCustomItem => designer.SelectedDashboardItem as CustomDashboardItem<HeatmapItemMetadata>;
        IDashboardControl dashboardControl;
        DashboardDesigner designer { get { return dashboardControl as DashboardDesigner; } }
        public void AttachViewer(DashboardViewer viewer) {
            AttachDashboardControl(viewer);
        }
        public void DetachViewer() {
            Detach();
        }
        public void AttachDesigner(DashboardDesigner designer) {
            AttachDashboardControl(designer);
            designer.CreateCustomItemBars(typeof(HeatmapItemMetadata));
            designer.DashboardCustomPropertyChanged += UpdateBarsEventHandler;
            designer.DashboardItemSelected += UpdateBarsEventHandler;
            InitializeBarItems();
        }
        public void DetachDesigner() {
            designer.DashboardCustomPropertyChanged -= UpdateBarsEventHandler;
            designer.DashboardItemSelected -= UpdateBarsEventHandler;
            Detach();
        }
        void Detach() {
            if(dashboardControl != null) 
                dashboardControl.CustomDashboardItemControlCreating -= OnCustomDashboardItemControlCreating;
            dashboardControl = null;
        }
        void AttachDashboardControl(IDashboardControl dashboardControl) {
            if(dashboardControl != null) {
                this.dashboardControl = dashboardControl;
                dashboardControl.CustomDashboardItemControlCreating += OnCustomDashboardItemControlCreating;
            }
        }
        void InitializeBarItems() {
            RibbonPage page = designer.Ribbon.GetDashboardRibbonPage(typeof(HeatmapItemMetadata), DashboardRibbonPage.Design);
            RibbonPageGroup coloringGroup = page.GetGroupByName("Coloring");
            if(coloringGroup == null) {
                coloringGroup = new RibbonPageGroup("Coloring") { Name = "Coloring" };
                page.Groups.Add(coloringGroup);
                coloringGroup.AllowTextClipping = false;
            }
            editColorsBarItem = new BarButtonItem(designer.Ribbon.Manager, "Edit Colors");
            editColorsBarItem.ImageOptions.SvgImage = SvgImage.FromResources("DevExpresss.DashboardWin.CustomItemExtension.Images.Heatmap_ColorScheme.svg", this.GetType().Assembly);
            editColorsBarItem.ItemClick += OptionsBarItem_ItemClick;
            coloringGroup.ItemLinks.Add(editColorsBarItem);
            RibbonPageGroup group = page.GetGroupByName("Diagram");
            if(group == null) {
                group = new RibbonPageGroup("Diagram") { Name = "Diagram" };
                page.Groups.Add(group);
                group.AllowTextClipping = false;
            }
            barShowLabelsItem = new BarCheckItem(designer.Ribbon.Manager);
            barShowLabelsItem.Checked = false;
            barShowLabelsItem.ImageOptions.SvgImage = SvgImage.FromResources("DevExpresss.DashboardWin.CustomItemExtension.Images.Heatmap_ShowLabels.svg", this.GetType().Assembly);
            barShowLabelsItem.Caption = "Show Labels";
            barShowLabelsItem.ItemClick += (s, e) => {
                if(SelectedCustomItem != null)
                    SelectedCustomItem.Metadata.ShowLabels = !SelectedCustomItem.Metadata.ShowLabels;
            };
            barShowLegendItem = new BarCheckItem(designer.Ribbon.Manager);
            barShowLegendItem.Checked = false;
            barShowLegendItem.ImageOptions.SvgImage = SvgImage.FromResources("DevExpresss.DashboardWin.CustomItemExtension.Images.Heatmap_ShowLegend.svg", this.GetType().Assembly);
            barShowLegendItem.Caption = "Show Legend";
            barShowLegendItem.ItemClick += (s, e) => {
                if(SelectedCustomItem != null)
                    SelectedCustomItem.Metadata.ShowLegend = !SelectedCustomItem.Metadata.ShowLegend;
            };
            barEnableZoomXAxisItem = new BarCheckItem(designer.Ribbon.Manager);
            barEnableZoomXAxisItem.Checked = false;
            barEnableZoomXAxisItem.ImageOptions.SvgImage = SvgImage.FromResources("DevExpresss.DashboardWin.CustomItemExtension.Images.Heatmap_ScrollHorizontal.svg", this.GetType().Assembly);
            barEnableZoomXAxisItem.Caption = "Enable Zooming X-Axis";
            barEnableZoomXAxisItem.ItemClick += (s, e) => {
                if(SelectedCustomItem != null)
                    SelectedCustomItem.Metadata.EnableZoomingXAxis = !SelectedCustomItem.Metadata.EnableZoomingXAxis;
            };
            barEnableZoomYAxisItem = new BarCheckItem(designer.Ribbon.Manager);
            barEnableZoomYAxisItem.Checked = false;
            barEnableZoomYAxisItem.ImageOptions.SvgImage = SvgImage.FromResources("DevExpresss.DashboardWin.CustomItemExtension.Images.Heatmap_ScrollVertical.svg", this.GetType().Assembly);
            barEnableZoomYAxisItem.Caption = "Enable Zooming Y-Axis";
            barEnableZoomYAxisItem.ItemClick += (s, e) => {
                if(SelectedCustomItem != null)
                    SelectedCustomItem.Metadata.EnableZoomingYAxis = !SelectedCustomItem.Metadata.EnableZoomingYAxis;
            };
            group.ItemLinks.Add(barShowLabelsItem);
            group.ItemLinks.Add(barShowLegendItem);
            group.ItemLinks.Add(barEnableZoomXAxisItem);
            group.ItemLinks.Add(barEnableZoomYAxisItem);
        }
        void UpdateBarsEventHandler(object sender, EventArgs e) {
            if(SelectedCustomItem != null) {
                barShowLabelsItem.Checked = SelectedCustomItem.Metadata.ShowLabels;
                barShowLegendItem.Checked = SelectedCustomItem.Metadata.ShowLegend;
                barEnableZoomXAxisItem.Checked = SelectedCustomItem.Metadata.EnableZoomingXAxis;
                barEnableZoomYAxisItem.Checked = SelectedCustomItem.Metadata.EnableZoomingYAxis;
            }
        }
        private void OptionsBarItem_ItemClick(object sender, ItemClickEventArgs e) {
            using(HeatmapOptionsForm dlg = new HeatmapOptionsForm(SelectedCustomItem.Metadata.Palette, SelectedCustomItem.Metadata.Scale)) {
                if(dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    SelectedCustomItem.Metadata.Palette = dlg.GetPalette();
                    SelectedCustomItem.Metadata.Scale = dlg.GetScale();
                }
            }
        }
        void OnCustomDashboardItemControlCreating(object sender, CustomDashboardItemControlCreatingEventArgs e) {
            IDashboardControl dashboardControl = (IDashboardControl)sender;
            if(e.MetadataType == typeof(HeatmapItemMetadata))
                e.CustomControlProvider = new HeatmapItemControlProvider(
                    dashboardControl.Dashboard.Items[e.DashboardItemName] as CustomDashboardItem<HeatmapItemMetadata>);
        }
    }
}
