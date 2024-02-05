using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace DevExpress.DashboardWin.CustomItemExtension {
    public class SunburstItemExtensionModule : IExtensionModule {
        IDashboardControl dashboardControl;
        public void AttachViewer(DashboardViewer viewer) {
            AttachDashboardControl(viewer);
        }
        public void DetachViewer() {
            Detach();
        }
        public void AttachDesigner(DashboardDesigner designer) {
            AttachDashboardControl(designer);
            designer.CreateCustomItemBars(typeof(SunburstItemMetadata));
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
                dashboardControl.CalculateHiddenTotals = true;
                dashboardControl.CustomDashboardItemControlCreating += OnCustomDashboardItemControlCreating;
            }
        }
        void OnCustomDashboardItemControlCreating(object sender, CustomDashboardItemControlCreatingEventArgs e) {
            IDashboardControl dashboardControl = (IDashboardControl)sender;
            if(e.MetadataType == typeof(SunburstItemMetadata))
                e.CustomControlProvider = new SunburstItemControlProvider(
                    dashboardControl.Dashboard.Items[e.DashboardItemName] as CustomDashboardItem<SunburstItemMetadata>);
        }
        void RemoveDrillDownBarItem(DashboardDesigner designer) {
            RibbonPage page = designer.Ribbon.GetDashboardRibbonPage(typeof(SunburstItemMetadata), DashboardRibbonPage.Data);
            RibbonPageGroup interactivityGroup = page.Groups[1];
            BarItem drillDownBarItem = interactivityGroup.ItemLinks[2].Item;
            interactivityGroup.ItemLinks.Remove(drillDownBarItem);
        }
    }
}
