Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports DevExpress.XtraGantt
Imports DevExpress.XtraPrinting.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraTreeList
Imports System
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms

Namespace DevExpresss.DashboardWin.CustomItemExtension
	Public Class GanttItemControlProvider
		Inherits CustomControlProviderBase

		Private supportedDateGroupIntervals() As DateTimeGroupInterval = { DateTimeGroupInterval.DayMonthYear, DateTimeGroupInterval.MonthYear, DateTimeGroupInterval.QuarterYear, DateTimeGroupInterval.WeekYear, DateTimeGroupInterval.DateHour, DateTimeGroupInterval.DateHourMinute, DateTimeGroupInterval.DateHourMinuteSecond, DateTimeGroupInterval.None }
		Private validationError As String = String.Empty
		Private dashboardItem As CustomDashboardItem(Of GanttItemMetadata)
		Private gantt As GanttControl
		Private flatData As DashboardFlatDataSource
		Protected Overrides ReadOnly Property Control() As Control
			Get
				Return gantt
			End Get
		End Property
		Private skipMasterFiltering As Boolean = False

		Public Sub New(ByVal dashboardItem As CustomDashboardItem(Of GanttItemMetadata))
			Me.dashboardItem = dashboardItem
			gantt = New GanttControl()
			AddHandler gantt.BeforeCheckNode, AddressOf Gantt_BeforeCheckNode
			AddHandler gantt.AfterCheckNode, AddressOf Gantt_AfterCheckNode
			AddHandler gantt.CustomDrawTask, AddressOf Gantt_CustomDrawTask
			AddHandler gantt.CustomPrintTask, AddressOf Gantt_CustomPrintTask
			AddHandler gantt.CustomTaskDisplayText, AddressOf Gantt_CustomTaskDisplayText
			AddHandler gantt.CustomColumnDisplayText, AddressOf Gantt_CustomColumnDisplayText
			AddHandler gantt.CustomDrawEmptyArea, AddressOf Gantt_CustomDrawEmptyArea
			gantt.OptionsView.ShowBaselines = True
			gantt.OptionsBehavior.Editable = False
			gantt.OptionsView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus
			gantt.OptionsSelection.EnableAppearanceFocusedCell = False
			gantt.OptionsSelection.EnableAppearanceFocusedRow = False
			gantt.OptionsSelection.EnableAppearanceHotTrackedRow = DevExpress.Utils.DefaultBoolean.False
			gantt.OptionsMenu.EnableColumnMenu = False
			gantt.OptionsMenu.EnableFooterMenu = False
			gantt.OptionsMenu.EnableNodeMenu = False
			gantt.OptionsFind.AllowFindPanel = False
			gantt.OptionsCustomization.AllowColumnMoving = False
			gantt.OptionsCustomization.AllowColumnResizing = False
			gantt.OptionsCustomization.AllowFilter = False
			gantt.OptionsCustomization.AllowQuickHideColumns = False
			gantt.OptionsCustomization.AllowSort = False
		End Sub

		Private Sub Gantt_CustomDrawEmptyArea(ByVal sender As Object, ByVal e As CustomDrawEmptyAreaEventArgs)
			If gantt.Nodes.Count > 1 Then
				Return
			End If
			e.DefaultDraw()
			e.Cache.DrawString(validationError, e.Appearance.Font, e.Cache.GetSolidBrush(Color.Black), e.Bounds)
			e.Handled = True
		End Sub

		Private Sub Gantt_CustomTaskDisplayText(ByVal sender As Object, ByVal e As CustomTaskDisplayTextEventArgs)
			If dashboardItem.Metadata.Text IsNot Nothing Then
				Dim row = TryCast(gantt.GetDataRecordByNode(e.Node), DashboardFlatDataSourceRow)
				e.RightText = flatData.GetDisplayText(dashboardItem.Metadata.Text.UniqueId, row)
			End If
		End Sub

		Private Sub Gantt_CustomPrintTask(ByVal sender As Object, ByVal e As DevExpress.XtraGantt.Printing.CustomPrintTaskEventArgs)
			Dim row As DashboardFlatDataSourceRow = TryCast(gantt.GetDataRecordByNode(e.Node), DashboardFlatDataSourceRow)
			Dim colorData As Integer = DirectCast(flatData.GetValue(flatData.GetColoringColumn().Name, row), Integer)
			e.Appearance.BackColor = Color.FromArgb(colorData)
		End Sub

		Private Sub Gantt_CustomDrawTask(ByVal sender As Object, ByVal e As CustomDrawTaskEventArgs)
			Dim row As DashboardFlatDataSourceRow = TryCast(gantt.GetDataRecordByNode(e.Node), DashboardFlatDataSourceRow)
			Dim colorData As Integer = DirectCast(flatData.GetValue(flatData.GetColoringColumn().Name, row), Integer)
			e.Appearance.BackColor = Color.FromArgb(colorData)
		End Sub

		Private Sub Gantt_CustomColumnDisplayText(ByVal sender As Object, ByVal e As DevExpress.XtraTreeList.CustomColumnDisplayTextEventArgs)
			Dim row = TryCast(gantt.GetDataRecordByNode(e.Node), DashboardFlatDataSourceRow)
			e.DisplayText = flatData.GetDisplayText(e.Column.FieldName, row)
		End Sub

		Protected Overrides Sub UpdateControl(ByVal customItemData As CustomItemData)
			skipMasterFiltering = True
			gantt.DataSource = Nothing
			gantt.Columns.Clear()
			gantt.ChartMappings.FinishDateFieldName = String.Empty
			gantt.ChartMappings.StartDateFieldName = gantt.ChartMappings.FinishDateFieldName
			gantt.ChartMappings.TextFieldName = gantt.ChartMappings.StartDateFieldName
			gantt.TreeListMappings.ParentFieldName = gantt.ChartMappings.TextFieldName
			gantt.TreeListMappings.KeyFieldName = gantt.TreeListMappings.ParentFieldName
			If Not ValidateBindings() Then
				Return
			End If
			SetDataBindings(customItemData)
			SetSelectionMode()
			skipMasterFiltering = False
		End Sub

		Private Sub SetDataBindings(ByVal customItemData As CustomItemData)
			Try
				flatData = customItemData.GetFlatData(New DashboardFlatDataSourceOptions() With {.AddColoringColumns = True})
				gantt.TreeListMappings.KeyFieldName = dashboardItem.Metadata.ID.UniqueId
				gantt.TreeListMappings.ParentFieldName = dashboardItem.Metadata.ParentID.UniqueId
				gantt.ChartMappings.TextFieldName = dashboardItem.Metadata.Text.UniqueId
				gantt.ChartMappings.StartDateFieldName = dashboardItem.Metadata.StartDate.UniqueId
				gantt.ChartMappings.FinishDateFieldName = dashboardItem.Metadata.FinishDate.UniqueId

				gantt.DataSource = flatData
				gantt.PopulateColumns()
				gantt.ExpandAll()

				For i As Integer = 0 To gantt.Columns.Count - 1
					Dim column = gantt.Columns(i)

					column.Visible = True

					If column.FieldName <> dashboardItem.Metadata?.Text?.UniqueId AndAlso column.FieldName <> dashboardItem.Metadata?.StartDate?.UniqueId AndAlso column.FieldName <> dashboardItem.Metadata?.FinishDate?.UniqueId Then
						column.Visible = False
					End If
				Next i
				skipMasterFiltering = False
			Catch
				gantt.DataSource = Nothing
			End Try
		End Sub

		Private Function ValidateBindings() As Boolean
			validationError = String.Empty
			If dashboardItem.Metadata.ID Is Nothing OrElse dashboardItem.Metadata.ParentID Is Nothing Then
				validationError = "Add the ID and Parent ID dimensions"
				Return False
			End If
			If dashboardItem.Metadata.ID.DataMember = dashboardItem.Metadata.ParentID.DataMember Then
				validationError = "Add different data fields to the ID and Parent ID sections"
				Return False
			End If
			If dashboardItem.Metadata.StartDate Is Nothing OrElse dashboardItem.Metadata.FinishDate Is Nothing Then
				validationError = "Add the Start Date and Finish Date dimensions"
				Return False
			End If
			If dashboardItem.Metadata.StartDate.DataMember = dashboardItem.Metadata.FinishDate.DataMember Then
				validationError = "Add different data fields to the Start Date and Finish Date sections"
				Return False
			End If

			If Not supportedDateGroupIntervals.Contains(dashboardItem.Metadata.StartDate.DateTimeGroupInterval) Then
				validationError = "The Gantt Item does not support this group interval in the Start Date data section."
				Return False
			End If

			If Not supportedDateGroupIntervals.Contains(dashboardItem.Metadata.FinishDate.DateTimeGroupInterval) Then
				validationError = "The Gantt Item does not support this group interval in the Finish Date data section."
				Return False
			End If

			If (dashboardItem.Metadata.ID.TopNOptions.Enabled AndAlso dashboardItem.Metadata.ID.TopNOptions.ShowOthers) OrElse (dashboardItem.Metadata.ParentID.TopNOptions.Enabled AndAlso dashboardItem.Metadata.ParentID.TopNOptions.ShowOthers) Then
				validationError = "The Gantt Item does not support the ""Show Others value"" option in the ID and Parent ID data sections"
				Return False
			End If
			If (dashboardItem.Metadata.StartDate.TopNOptions.Enabled AndAlso dashboardItem.Metadata.StartDate.TopNOptions.ShowOthers) OrElse (dashboardItem.Metadata.FinishDate.TopNOptions.Enabled AndAlso dashboardItem.Metadata.FinishDate.TopNOptions.ShowOthers) Then
				validationError = "The Gantt Item does not support the ""Show Others value"" option in the Start Date and Finish Date data sections"
				Return False
			End If
			If dashboardItem.InteractivityOptions.IsDrillDownEnabled Then
				validationError = "The Gantt Item does not support Drill-Down"
				Return False
			End If
			If dashboardItem.Metadata.Text Is Nothing Then
				Return False
			End If
			Return True

		End Function
		Protected Overrides Function GetPrintableControl(ByVal customItemData As CustomItemData, ByVal exportInfo As CustomItemExportInfo) As XRControl
			Dim bitmap As New Bitmap(gantt.Width, gantt.Height)
			gantt.DrawToBitmap(bitmap, gantt.Bounds)
			Dim xRPictureBox As New XRPictureBox()
			xRPictureBox.ImageSource = New ImageSource(bitmap)
			Return xRPictureBox
		End Function
		Protected Overrides Sub SetSelection(ByVal selection As CustomItemSelection)
			skipMasterFiltering = True

			gantt.ClearSelection()

			For Each item As Object() In selection.GetValues()
				If item.Length > 0 AndAlso dashboardItem.Metadata.ID IsNot Nothing Then
					Dim node = gantt.FindNodeByKeyID(item(0))
					node.CheckState = CheckState.Checked
				End If
			Next item
			skipMasterFiltering = False
		End Sub
		Private Sub Gantt_BeforeCheckNode(ByVal sender As Object, ByVal e As CheckNodeEventArgs)
			If gantt.OptionsView.CheckBoxStyle = DefaultNodeCheckBoxStyle.Radio Then
				skipMasterFiltering = True
				gantt.UncheckAll()
				skipMasterFiltering = False
			End If
		End Sub
		Private Sub Gantt_AfterCheckNode(ByVal sender As Object, ByVal e As NodeEventArgs)
			If skipMasterFiltering Then
				Return
			End If
			If gantt.GetAllCheckedNodes().Count = 0 Then
				Interactivity.ClearMasterFilter()
			Else
				Dim flatDataRows = gantt.GetAllCheckedNodes().Select(Function(node) gantt.GetDataRecordByNode(node)).Cast(Of DashboardFlatDataSourceRow)()
				Interactivity.SetMasterFilter(flatDataRows)
			End If
		End Sub
		Private Sub SetSelectionMode()
			Select Case dashboardItem.InteractivityOptions.MasterFilterMode
				Case DashboardItemMasterFilterMode.None
					gantt.OptionsView.CheckBoxStyle = DefaultNodeCheckBoxStyle.Default
					Return
				Case DashboardItemMasterFilterMode.Multiple
					gantt.OptionsView.CheckBoxStyle = DefaultNodeCheckBoxStyle.Check
				Case DashboardItemMasterFilterMode.Single
					gantt.OptionsView.CheckBoxStyle = DefaultNodeCheckBoxStyle.Radio
			End Select
		End Sub
	End Class
End Namespace
