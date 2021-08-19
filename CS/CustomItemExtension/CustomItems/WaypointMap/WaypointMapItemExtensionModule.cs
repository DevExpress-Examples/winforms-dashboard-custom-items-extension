using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace DevExpresss.DashboardWin.CustomItemExtension {
    public class WaypointMapItemExtensionModule:IExtensionModule  {
        IDashboardControl dashboardControl;
        string bingKey;

        public WaypointMapItemExtensionModule(string bingKey) {
            this.bingKey = bingKey;
        }
        public void AttachViewer(DashboardViewer viewer) {
            AttachDashboardControl(viewer);
        }
        public void DetachViewer() {
            Detach();
        }
        public void AttachDesigner(DashboardDesigner designer) {
            AttachDashboardControl(designer);
            designer.CreateCustomItemBars(typeof(WaypointMapItemMetadata));
            RemoveDrillDownBarItem(designer);
        }
        public void DetachDesigner() {
            Detach();
        }
        void Detach() {
            if(dashboardControl != null)
                dashboardControl.CustomDashboardItemControlCreating -= OnCustomDashboardItemControlCreating;
        }
        void AttachDashboardControl(IDashboardControl dashboardControl) {
            if(dashboardControl != null) {
                this.dashboardControl = dashboardControl;
                dashboardControl.CustomDashboardItemControlCreating += OnCustomDashboardItemControlCreating;
            }
        }
        void OnCustomDashboardItemControlCreating(object sender, CustomDashboardItemControlCreatingEventArgs e) {
            IDashboardControl dashboardControl = (IDashboardControl)sender;
            if(e.MetadataType == typeof(WaypointMapItemMetadata))
                e.CustomControlProvider = new WaypointMapItemControlProvider(dashboardControl.Dashboard.Items[e.DashboardItemName] as CustomDashboardItem<WaypointMapItemMetadata>, bingKey);
        }
        void RemoveDrillDownBarItem(DashboardDesigner designer) {
            RibbonPage page = designer.Ribbon.GetDashboardRibbonPage(typeof(WaypointMapItemMetadata), DashboardRibbonPage.Data);
            RibbonPageGroup interactivityGroup = page.Groups[1];
            BarItem drillDownBarItem = interactivityGroup.ItemLinks[2].Item;
            interactivityGroup.ItemLinks.Remove(drillDownBarItem);
        }
    }
}
