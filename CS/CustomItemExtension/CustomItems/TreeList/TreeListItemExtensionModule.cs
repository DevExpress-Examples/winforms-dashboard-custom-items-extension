using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace DevExpresss.DashboardWin.CustomItemExtension
{
    public class TreeListItemExtensionModule {
        IDashboardControl dashboardControl;
        public void AttachViewer(DashboardViewer viewer) {
            AttachDashboardControl(viewer);
        }
        public void DetachViewer() {
            Detach();
        }
        public void AttachDesigner(DashboardDesigner designer) {
            AttachDashboardControl(designer);
            designer.CreateCustomItemBars(typeof(TreeListItemMetadata));
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
            if(e.MetadataType == typeof(TreeListItemMetadata))
                e.CustomControlProvider = new TreeListItemControlProvider(dashboardControl.Dashboard.Items[e.DashboardItemName] as CustomDashboardItem<TreeListItemMetadata>);
        }
        void RemoveDrillDownBarItem(DashboardDesigner designer) {
            RibbonPage page = designer.Ribbon.GetDashboardRibbonPage(typeof(TreeListItemMetadata), DashboardRibbonPage.Data);
            RibbonPageGroup interactivityGroup = page.Groups[1];
            BarItem drillDownBarItem = interactivityGroup.ItemLinks[2].Item;
            interactivityGroup.ItemLinks.Remove(drillDownBarItem);
        }

    }
}
