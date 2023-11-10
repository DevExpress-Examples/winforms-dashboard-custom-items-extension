using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using DevExpress.XtraGantt;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DevExpresss.DashboardWin.CustomItemExtension {
    public class GanttItemControlProvider : CustomControlProviderBase {
        DateTimeGroupInterval[] supportedDateGroupIntervals = new DateTimeGroupInterval[] {
            DateTimeGroupInterval.DayMonthYear,
            DateTimeGroupInterval.MonthYear,
            DateTimeGroupInterval.QuarterYear,
            DateTimeGroupInterval.WeekYear,
            DateTimeGroupInterval.DateHour,
            DateTimeGroupInterval.DateHourMinute,
            DateTimeGroupInterval.DateHourMinuteSecond,
            DateTimeGroupInterval.None
            };
        string validationError = string.Empty;
        CustomDashboardItem<GanttItemMetadata> dashboardItem;
        GanttControl gantt;
        DashboardFlatDataSource flatData;
        protected override Control Control { get { return gantt; } }
        bool skipMasterFiltering = false;

        public GanttItemControlProvider(CustomDashboardItem<GanttItemMetadata> dashboardItem) {
            this.dashboardItem = dashboardItem;
            gantt = new GanttControl();
            gantt.BeforeCheckNode += Gantt_BeforeCheckNode;
            gantt.AfterCheckNode += Gantt_AfterCheckNode;
            gantt.CustomDrawTask += Gantt_CustomDrawTask;
            gantt.CustomPrintTask += Gantt_CustomPrintTask;
            gantt.CustomTaskDisplayText += Gantt_CustomTaskDisplayText;
            gantt.CustomColumnDisplayText += Gantt_CustomColumnDisplayText;
            gantt.CustomDrawEmptyArea += Gantt_CustomDrawEmptyArea;
            gantt.OptionsView.ShowBaselines = true;
            gantt.OptionsBehavior.Editable = false;
            gantt.OptionsView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;
            gantt.OptionsSelection.EnableAppearanceFocusedCell = false;
            gantt.OptionsSelection.EnableAppearanceFocusedRow = false;
            gantt.OptionsSelection.EnableAppearanceHotTrackedRow =  DevExpress.Utils.DefaultBoolean.False;
            gantt.OptionsMenu.EnableColumnMenu = false;
            gantt.OptionsMenu.EnableFooterMenu = false;
            gantt.OptionsMenu.EnableNodeMenu = false;
            gantt.OptionsFind.AllowFindPanel = false;
            gantt.OptionsCustomization.AllowColumnMoving = false;
            gantt.OptionsCustomization.AllowColumnResizing = false;
            gantt.OptionsCustomization.AllowFilter = false;
            gantt.OptionsCustomization.AllowQuickHideColumns = false;
            gantt.OptionsCustomization.AllowSort = false;
        }

        private void Gantt_CustomDrawEmptyArea(object sender, CustomDrawEmptyAreaEventArgs e) {
            if(gantt.Nodes.Count > 1) return;
            e.DefaultDraw();
            e.Cache.DrawString(validationError, e.Appearance.Font, e.Cache.GetSolidBrush(Color.Black), e.Bounds);
            e.Handled = true;
        }

        private void Gantt_CustomTaskDisplayText(object sender, CustomTaskDisplayTextEventArgs e)
        {
            if (dashboardItem.Metadata.Text != null)
            {
                var row = gantt.GetDataRecordByNode(e.Node) as DashboardFlatDataSourceRow;
                e.RightText = flatData.GetDisplayText(dashboardItem.Metadata.Text.UniqueId, row);
            }
        }

        private void Gantt_CustomPrintTask(object sender, DevExpress.XtraGantt.Printing.CustomPrintTaskEventArgs e)
        {
            DashboardFlatDataSourceRow row = gantt.GetDataRecordByNode(e.Node) as DashboardFlatDataSourceRow;
            int colorData = (int)flatData.GetValue(flatData.GetColoringColumn().Name, row);
            e.Appearance.BackColor = Color.FromArgb(colorData);
        }

        private void Gantt_CustomDrawTask(object sender, CustomDrawTaskEventArgs e)
        {
            DashboardFlatDataSourceRow row = gantt.GetDataRecordByNode(e.Node) as DashboardFlatDataSourceRow;
            int colorData = (int)flatData.GetValue(flatData.GetColoringColumn().Name, row);
            e.Appearance.BackColor = Color.FromArgb(colorData);
        }

        private void Gantt_CustomColumnDisplayText(object sender, DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs e) {
            var row = gantt.GetDataRecordByNode(e.Node) as DashboardFlatDataSourceRow;
            e.DisplayText = flatData.GetDisplayText(e.Column.FieldName, row);
        }

        protected override void UpdateControl(CustomItemData customItemData) {
            skipMasterFiltering = true;
            gantt.DataSource = null;
            gantt.Columns.Clear();
            gantt.TreeListMappings.KeyFieldName = gantt.TreeListMappings.ParentFieldName = gantt.ChartMappings.TextFieldName 
                = gantt.ChartMappings.StartDateFieldName = gantt.ChartMappings.FinishDateFieldName = string.Empty;
            if(!ValidateBindings()) return;
            SetDataBindings(customItemData);
            SetSelectionMode();
            skipMasterFiltering = false;
        }

        void SetDataBindings(CustomItemData customItemData) {
            try {
                flatData = customItemData.GetFlatData(new DashboardFlatDataSourceOptions() { AddColoringColumns = true });
                gantt.TreeListMappings.KeyFieldName = dashboardItem.Metadata.ID.UniqueId;
                gantt.TreeListMappings.ParentFieldName = dashboardItem.Metadata.ParentID.UniqueId;
                gantt.ChartMappings.TextFieldName = dashboardItem.Metadata.Text.UniqueId;
                gantt.ChartMappings.StartDateFieldName = dashboardItem.Metadata.StartDate.UniqueId;
                gantt.ChartMappings.FinishDateFieldName = dashboardItem.Metadata.FinishDate.UniqueId;

                gantt.DataSource = flatData;
                gantt.PopulateColumns();
                gantt.ExpandAll();

                for(int i = 0; i < gantt.Columns.Count; i++) {
                    var column = gantt.Columns[i];

                    column.Visible = true;

                    if(column.FieldName != dashboardItem.Metadata?.Text?.UniqueId
                        && column.FieldName != dashboardItem.Metadata?.StartDate?.UniqueId
                        && column.FieldName != dashboardItem.Metadata?.FinishDate?.UniqueId)
                        column.Visible = false;
                }
                skipMasterFiltering = false;
            }
            catch {
                gantt.DataSource = null;
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
            if(dashboardItem.Metadata.StartDate == null
                || dashboardItem.Metadata.FinishDate == null) {
                validationError = "Add the Start Date and Finish Date dimensions";
                return false;
            };
            if(dashboardItem.Metadata.StartDate.DataMember == dashboardItem.Metadata.FinishDate.DataMember) {
                validationError = "Add different data fields to the Start Date and Finish Date sections";
                return false;
            };

            if(!supportedDateGroupIntervals.Contains(dashboardItem.Metadata.StartDate.DateTimeGroupInterval)) { 
                validationError = "The Gantt Item does not support this group interval in the Start Date data section.";
                return false;
            };

            if(!supportedDateGroupIntervals.Contains(dashboardItem.Metadata.FinishDate.DateTimeGroupInterval)) {
                validationError = "The Gantt Item does not support this group interval in the Finish Date data section.";
                return false;
            };

            if( (dashboardItem.Metadata.ID.TopNOptions.Enabled && dashboardItem.Metadata.ID.TopNOptions.ShowOthers)
                || (dashboardItem.Metadata.ParentID.TopNOptions.Enabled && dashboardItem.Metadata.ParentID.TopNOptions.ShowOthers)) {
                validationError = "The Gantt Item does not support the \"Show Others value\" option in the ID and Parent ID data sections";
                return false;
            }
            if((dashboardItem.Metadata.StartDate.TopNOptions.Enabled && dashboardItem.Metadata.StartDate.TopNOptions.ShowOthers)
                || (dashboardItem.Metadata.FinishDate.TopNOptions.Enabled && dashboardItem.Metadata.FinishDate.TopNOptions.ShowOthers)) {
                validationError = "The Gantt Item does not support the \"Show Others value\" option in the Start Date and Finish Date data sections";
                return false;
            }
            if(dashboardItem.InteractivityOptions.IsDrillDownEnabled) {
                validationError = "The Gantt Item does not support Drill-Down";
                return false;
            }
            if (dashboardItem.Metadata.Text == null) {
                return false;
            }
            return true;

        }
        protected override XRControl GetPrintableControl(CustomItemData customItemData, CustomItemExportInfo exportInfo) {
            Bitmap bitmap = new Bitmap(gantt.Width, gantt.Height);
            gantt.DrawToBitmap(bitmap, gantt.Bounds);
            XRPictureBox xRPictureBox = new XRPictureBox();
            xRPictureBox.ImageSource = new ImageSource(bitmap);
            return xRPictureBox;
        }
        protected override void SetSelection(CustomItemSelection selection) {
            skipMasterFiltering = true;

            gantt.ClearSelection();

            foreach (object[] item in selection.GetValues()) {
                if (item.Length > 0 && dashboardItem.Metadata.ID != null) {
                    var node = gantt.FindNodeByKeyID(item[0]);
                    node.CheckState = CheckState.Checked;
                }
            }
            skipMasterFiltering = false;
        }
        private void Gantt_BeforeCheckNode(object sender, CheckNodeEventArgs e) {
            if (gantt.OptionsView.CheckBoxStyle == DefaultNodeCheckBoxStyle.Radio) {
                skipMasterFiltering = true;
                gantt.UncheckAll();
                skipMasterFiltering = false;
            }
        }
        private void Gantt_AfterCheckNode(object sender, NodeEventArgs e) {
            if (skipMasterFiltering) return;
            if (gantt.GetAllCheckedNodes().Count == 0)
                Interactivity.ClearMasterFilter();
            else {
                var flatDataRows = gantt.GetAllCheckedNodes().Select(node => gantt.GetDataRecordByNode(node)).Cast<DashboardFlatDataSourceRow>();
                Interactivity.SetMasterFilter(flatDataRows);
            }
        }
        void SetSelectionMode() {
            switch(dashboardItem.InteractivityOptions.MasterFilterMode) {
                case DashboardItemMasterFilterMode.None:
                    gantt.OptionsView.CheckBoxStyle = DefaultNodeCheckBoxStyle.Default;
                    return;
                case DashboardItemMasterFilterMode.Multiple:
                    gantt.OptionsView.CheckBoxStyle = DefaultNodeCheckBoxStyle.Check;
                    break;
                case DashboardItemMasterFilterMode.Single:
                    gantt.OptionsView.CheckBoxStyle = DefaultNodeCheckBoxStyle.Radio;
                    break;
            }
        }
    }
}
