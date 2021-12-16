
using DevExpress.DashboardCommon;
using DevExpress.XtraBars.Navigation;
using DevExpresss.DashboardWin.CustomItemExtension;
using System.IO;
using System.Linq;

namespace CustomItemTest {
    public partial class DesignerForm1 : DevExpress.XtraBars.Ribbon.RibbonForm {
        SunburstItemExtensionModule sunburstItemModule = new SunburstItemExtensionModule();
        FunnelItemExtensionModule funnelItemModule = new FunnelItemExtensionModule();
        SankeyItemExtensionModule sankeyItemModule = new SankeyItemExtensionModule();
        WaypointMapItemExtensionModule waypointMapItemModule = new WaypointMapItemExtensionModule("YOUR BING KEY");
        TreeListItemExtensionModule hierarchyTreeItemModule = new TreeListItemExtensionModule();
        GanttItemExtensionModule ganttItemExtensionModule = new GanttItemExtensionModule();
        WebPageItemExtensionModule webPageItemExtensionModule = new WebPageItemExtensionModule();
        HeatmapItemExtensionModule heatmapItemModule = new HeatmapItemExtensionModule();
        public DesignerForm1() {
            Dashboard.CustomItemMetadataTypes.Register<SunburstItemMetadata>();
            Dashboard.CustomItemMetadataTypes.Register<SankeyItemMetadata>();
            Dashboard.CustomItemMetadataTypes.Register<WaypointMapItemMetadata>();
            Dashboard.CustomItemMetadataTypes.Register<FunnelItemMetadata>();
            Dashboard.CustomItemMetadataTypes.Register<TreeListItemMetadata>();
            Dashboard.CustomItemMetadataTypes.Register<GanttItemMetadata>();
            Dashboard.CustomItemMetadataTypes.Register<WebPageItemMetadata>();
            Dashboard.CustomItemMetadataTypes.Register<HeatmapItemMetadata>();

            InitializeComponent();
            dashboardDesigner.CreateRibbon();
            AttachModules();
            GenerateAccordionElements();
            dashboardsAccordion.SelectedElement = dashboardsAccordion.Elements.Last();
            dashboardsAccordion.ScrollBarMode = ScrollBarMode.Auto;
        }

        void AttachModules() {
            sunburstItemModule.AttachDesigner(dashboardDesigner);
            funnelItemModule.AttachDesigner(dashboardDesigner);
            sankeyItemModule.AttachDesigner(dashboardDesigner);
            waypointMapItemModule.AttachDesigner(dashboardDesigner);
            hierarchyTreeItemModule.AttachDesigner(dashboardDesigner);
            ganttItemExtensionModule.AttachDesigner(dashboardDesigner);
            webPageItemExtensionModule.AttachDesigner(dashboardDesigner);
            heatmapItemModule.AttachDesigner(dashboardDesigner);
        }
        void GenerateAccordionElements() {
            foreach( string dashboardFile in Directory.GetFiles("Dashboards", "*.xml", SearchOption.TopDirectoryOnly)) {
                AccordionControlElement item = dashboardsAccordion.AddItem();
                item.Text = dashboardFile.Replace("Dashboards\\","").Replace(".xml","");
                item.Tag = dashboardFile;
            }

    }

        private void dashboardsAccordion_SelectedElementChanged(object sender, SelectedElementChangedEventArgs e) {
            dashboardDesigner.LoadDashboard(e.Element.Tag.ToString());
        }
    }
}
