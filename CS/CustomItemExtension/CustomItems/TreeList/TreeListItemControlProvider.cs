using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DevExpresss.DashboardWin.CustomItemExtension
{
    public class TreeListItemControlProvider : CustomControlProviderBase {
        string validationError = string.Empty;
        bool skipMasterFiltering = false;
        TreeList tree;
        DashboardFlatDataSource flatData;
        CustomDashboardItem<TreeListItemMetadata> dashboardItem;
        protected override Control Control => tree;
        public TreeListItemControlProvider(CustomDashboardItem<TreeListItemMetadata> dashboardItem)
        {
            this.dashboardItem = dashboardItem;
            tree = new TreeList();
            tree.OptionsBehavior.Editable = false;
            tree.OptionsView.ShowIndicator = false;
            tree.BeforeCheckNode += Tree_BeforeCheckNode;
            tree.AfterCheckNode += Tree_AfterCheckNode;
            tree.CustomColumnDisplayText += Tree_CustomColumnDisplayText;
            tree.CustomDrawEmptyArea += Tree_CustomDrawEmptyArea;
            tree.OptionsView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;
            tree.OptionsSelection.EnableAppearanceFocusedCell = false;
            tree.OptionsSelection.EnableAppearanceFocusedCell = false;
            tree.OptionsSelection.EnableAppearanceFocusedRow = false;
            tree.OptionsMenu.EnableColumnMenu = false;
            tree.OptionsMenu.EnableFooterMenu = false;
            tree.OptionsMenu.EnableNodeMenu = false;
            tree.OptionsFind.AllowFindPanel = false;
            tree.OptionsCustomization.AllowColumnMoving = false;
            tree.OptionsCustomization.AllowColumnResizing = false;
            tree.OptionsCustomization.AllowFilter = false;
            tree.OptionsCustomization.AllowQuickHideColumns = false;
            tree.OptionsCustomization.AllowSort = false;
            tree.OptionsPrint.AutoWidth = false;
            tree.OptionsPrint.PrintCheckBoxes = true;
        }

        private void Tree_CustomDrawEmptyArea(object sender, CustomDrawEmptyAreaEventArgs e) {
            if(tree.Nodes.Count > 1) return;
            e.DefaultDraw();
            e.Cache.DrawString(validationError, e.Appearance.Font, e.Cache.GetSolidBrush(Color.Black), e.Bounds);
            e.Handled = true;
        }

        private void Tree_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e) {
            var row = tree.GetDataRecordByNode(e.Node) as DashboardFlatDataSourceRow;
            e.DisplayText = flatData.GetDisplayText(e.Column.FieldName, row);
        }

        private void Tree_AfterCheckNode(object sender, NodeEventArgs e) {
            if(skipMasterFiltering) return;
            if(tree.GetAllCheckedNodes().Count == 0)
                Interactivity.ClearMasterFilter();
            else {
                var flatDataRows = tree.GetAllCheckedNodes().Select(node => tree.GetDataRecordByNode(node)).Cast<DashboardFlatDataSourceRow>();
                Interactivity.SetMasterFilter(flatDataRows);
            }
        }
        private void Tree_BeforeCheckNode(object sender, CheckNodeEventArgs e) {

            if(tree.OptionsView.CheckBoxStyle == DefaultNodeCheckBoxStyle.Radio) {
                skipMasterFiltering = true;
                tree.UncheckAll();
                skipMasterFiltering = false;
            }
        }
        protected override void UpdateControl(CustomItemData customItemData)
        {
            skipMasterFiltering = true;
            tree.DataSource = null;
            tree.ParentFieldName = tree.KeyFieldName = string.Empty;
            if(!ValidateBindings()) return;
            SetDataBindings(customItemData);
            SetSelectionMode();
            skipMasterFiltering = false;
        }
        void SetDataBindings(CustomItemData customItemData) {
            try {
                tree.DataSource = flatData = customItemData.GetFlatData();
                tree.PopulateColumns();
                tree.ParentFieldName = dashboardItem.Metadata.ParentID.UniqueId;
                tree.KeyFieldName = dashboardItem.Metadata.ID.UniqueId;
                tree.ExpandAll();
            }
            catch {
                tree.DataSource = flatData = null;
            }
        }
        bool ValidateBindings() {
            validationError = string.Empty;
            if(dashboardItem.Metadata.ID == null
                || dashboardItem.Metadata.ParentID == null) {
                validationError = "Add the ID and Parent ID dimensions";
                return false;
            }
            if(dashboardItem.Metadata.ID.DataMember == dashboardItem.Metadata.ParentID.DataMember) {
                validationError = "Add different data fields to the ID and Parent ID sections";
                return false;
            };

            if( (dashboardItem.Metadata.ID.TopNOptions.Enabled && dashboardItem.Metadata.ID.TopNOptions.ShowOthers)
                || (dashboardItem.Metadata.ParentID.TopNOptions.Enabled && dashboardItem.Metadata.ParentID.TopNOptions.ShowOthers)) {
                validationError = "The Tree List Item does not support the \"Show Others value\" option in the ID and Parent ID data sections";
                return false;
            }
            if(dashboardItem.InteractivityOptions.IsDrillDownEnabled) {
                validationError = "The Tree List Item does not support Drill-Down";
                return false;
            }
            if (dashboardItem.Metadata.Dimensions.Count == 0) {
                return false;
            }
            return true;
        }
        void SetSelectionMode() {
            switch(dashboardItem.InteractivityOptions.MasterFilterMode) {
                case DashboardItemMasterFilterMode.None:
                    tree.OptionsView.CheckBoxStyle = DefaultNodeCheckBoxStyle.Default;
                    return;
                case DashboardItemMasterFilterMode.Multiple:
                    tree.OptionsView.CheckBoxStyle = DefaultNodeCheckBoxStyle.Check;
                    break;
                case DashboardItemMasterFilterMode.Single:
                    tree.OptionsView.CheckBoxStyle = DefaultNodeCheckBoxStyle.Radio;
                    break;
            }
        }
        protected override XRControl GetPrintableControl(CustomItemData customItemData, CustomItemExportInfo exportInfo) {
            Bitmap bitmap = new Bitmap(tree.Width, tree.Height);
            tree.DrawToBitmap(bitmap, tree.Bounds);
            XRPictureBox xRPictureBox = new XRPictureBox();
            xRPictureBox.ImageSource = new ImageSource(bitmap);
            return xRPictureBox;
        }
        protected override void SetSelection(CustomItemSelection selection) {
            skipMasterFiltering = true;
            tree.UncheckAll();
            foreach(object[] item in selection.GetValues()) {
                object id = item[0];
                tree.FindNodeByFieldValue(dashboardItem.Metadata.ID.UniqueId, id).CheckState = CheckState.Checked;
            }
            skipMasterFiltering = false;
        }
    }
}
