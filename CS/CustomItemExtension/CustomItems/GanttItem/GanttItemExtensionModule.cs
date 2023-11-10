using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace DevExpresss.DashboardWin.CustomItemExtension
{

    public class GanttItemExtensionModule : IExtensionModule {
        IDashboardControl dashboardControl;
        public void AttachViewer(DashboardViewer viewer) {
            AttachDashboardControl(viewer);
        }
        public void DetachViewer() {
            Detach();
        }
        public void AttachDesigner(DashboardDesigner designer) {
            AttachDashboardControl(designer);
            designer.CreateCustomItemBars(typeof(GanttItemMetadata));
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
            if(e.MetadataType == typeof(GanttItemMetadata))
                e.CustomControlProvider = new GanttItemControlProvider(dashboardControl.Dashboard.Items[e.DashboardItemName] as CustomDashboardItem<GanttItemMetadata>);
        }
        void RemoveDrillDownBarItem(DashboardDesigner designer) {
            RibbonPage page = designer.Ribbon.GetDashboardRibbonPage(typeof(GanttItemMetadata), DashboardRibbonPage.Data);
            RibbonPageGroup interactivityGroup = page.Groups[1];
            BarItem drillDownBarItem = interactivityGroup.ItemLinks[2].Item;
            interactivityGroup.ItemLinks.Remove(drillDownBarItem);
        }
    }

}
