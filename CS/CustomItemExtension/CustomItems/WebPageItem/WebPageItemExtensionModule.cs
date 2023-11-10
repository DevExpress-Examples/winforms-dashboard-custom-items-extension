using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Repository;
using DevExpresss.DashboardWin.CustomItemExtension.CustomItems.WebPageItem;

namespace DevExpresss.DashboardWin.CustomItemExtension
{
    public class WebPageItemExtensionModule : IExtensionModule {

        BarEditItem urlBarItem;
        IDashboardControl dashboardControl;
        DashboardDesigner designer { get { return dashboardControl as DashboardDesigner; } }
        CustomDashboardItem<WebPageItemMetadata> selectedCustomItem {
            get { return designer.SelectedDashboardItem as CustomDashboardItem<WebPageItemMetadata>; }
        }
        public void AttachViewer(DashboardViewer viewer) {
            AttachDashboardControl(viewer);
        }
        public void AttachDesigner(DashboardDesigner designer) {
            AttachDashboardControl(designer);
            designer.CreateCustomItemBars(typeof(WebPageItemMetadata));
            InitializeUrlBarItem();
            designer.DashboardItemSelected += Designer_DashboardItemSelected;
            // Other code specific for Designer
        }

        private void Designer_DashboardItemSelected(object sender, DashboardItemSelectedEventArgs e) {
            if(designer.SelectedDashboardItem is CustomDashboardItem<WebPageItemMetadata>)
                urlBarItem.EditValue = selectedCustomItem.Metadata.URI;
        }

        void AttachDashboardControl(IDashboardControl dashboardControl) {
            if(dashboardControl != null) {
                this.dashboardControl = dashboardControl;
                dashboardControl.CustomDashboardItemControlCreating += OnCustomDashboardItemControlCreating;
            }
        }
        public void DetachViewer() {
            Detach();
        }
        public void DetachDesigner() {
            Detach();
        }
        void Detach() {
            if(dashboardControl != null)
                dashboardControl.CustomDashboardItemControlCreating -= OnCustomDashboardItemControlCreating;
        }
        void OnCustomDashboardItemControlCreating(object sender, CustomDashboardItemControlCreatingEventArgs e) {
            if(e.MetadataType == typeof(WebPageItemMetadata))
                e.CustomControlProvider = new WebPageItemControlProvider(dashboardControl.Dashboard.Items[e.DashboardItemName] as CustomDashboardItem<WebPageItemMetadata>);
        }
        void InitializeUrlBarItem() {
            RibbonPage page = designer.Ribbon.GetDashboardRibbonPage(typeof(WebPageItemMetadata), DashboardRibbonPage.Design);
            RibbonPageGroup group = page.GetGroupByName("URI");
            if(group == null) {
                group = new RibbonPageGroup("URI") { Name = "URI" };
                page.Groups.Add(group);
                group.AllowTextClipping = false;
            }

            RepositoryItemButtonEdit buttonEdit = new RepositoryItemButtonEdit();
            buttonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            buttonEdit.ButtonClick += ButtonEdit_ButtonClick;
            urlBarItem = new BarEditItem(designer.Ribbon.Manager, buttonEdit);
            urlBarItem.Caption = "URI Pattern";
            urlBarItem.EditWidth = 150;
            group.ItemLinks.Add(urlBarItem);
        }

        private void ButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            using(UriEditForm dlg = new UriEditForm()) {
                dlg.UriPattern = selectedCustomItem.Metadata.URI;
                if(dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    selectedCustomItem.Metadata.URI = dlg.UriPattern;
                    urlBarItem.EditValue = dlg.UriPattern;
                }
            }
        }
    }
}
