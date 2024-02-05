Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports DevExpress.XtraPrinting.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraTreeList
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms

Namespace DevExpress.DashboardWin.CustomItemExtension
	Public Class TreeListItemControlProvider
		Inherits CustomControlProviderBase

		Private validationError As String = String.Empty
		Private skipMasterFiltering As Boolean = False
		Private tree As TreeList
		Private flatData As DashboardFlatDataSource
		Private dashboardItem As CustomDashboardItem(Of TreeListItemMetadata)
		Protected Overrides ReadOnly Property Control() As Control
			Get
				Return tree
			End Get
		End Property
		Public Sub New(ByVal dashboardItem As CustomDashboardItem(Of TreeListItemMetadata))
			Me.dashboardItem = dashboardItem
			tree = New TreeList()
			tree.OptionsBehavior.Editable = False
			tree.OptionsView.ShowIndicator = False
			AddHandler tree.BeforeCheckNode, AddressOf Tree_BeforeCheckNode
			AddHandler tree.AfterCheckNode, AddressOf Tree_AfterCheckNode
			AddHandler tree.CustomColumnDisplayText, AddressOf Tree_CustomColumnDisplayText
			AddHandler tree.CustomDrawEmptyArea, AddressOf Tree_CustomDrawEmptyArea
			tree.OptionsView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus
			tree.OptionsSelection.EnableAppearanceFocusedCell = False
			tree.OptionsSelection.EnableAppearanceFocusedCell = False
			tree.OptionsSelection.EnableAppearanceFocusedRow = False
			tree.OptionsMenu.EnableColumnMenu = False
			tree.OptionsMenu.EnableFooterMenu = False
			tree.OptionsMenu.EnableNodeMenu = False
			tree.OptionsFind.AllowFindPanel = False
			tree.OptionsCustomization.AllowColumnMoving = False
			tree.OptionsCustomization.AllowColumnResizing = False
			tree.OptionsCustomization.AllowFilter = False
			tree.OptionsCustomization.AllowQuickHideColumns = False
			tree.OptionsCustomization.AllowSort = False
			tree.OptionsPrint.AutoWidth = False
			tree.OptionsPrint.PrintCheckBoxes = True
		End Sub

		Private Sub Tree_CustomDrawEmptyArea(ByVal sender As Object, ByVal e As CustomDrawEmptyAreaEventArgs)
			If tree.Nodes.Count > 1 Then
				Return
			End If
			e.DefaultDraw()
			e.Cache.DrawString(validationError, e.Appearance.Font, e.Cache.GetSolidBrush(Color.Black), e.Bounds)
			e.Handled = True
		End Sub

		Private Sub Tree_CustomColumnDisplayText(ByVal sender As Object, ByVal e As CustomColumnDisplayTextEventArgs)
			Dim row = TryCast(tree.GetDataRecordByNode(e.Node), DashboardFlatDataSourceRow)
			e.DisplayText = flatData.GetDisplayText(e.Column.FieldName, row)
		End Sub

		Private Sub Tree_AfterCheckNode(ByVal sender As Object, ByVal e As NodeEventArgs)
			If skipMasterFiltering Then
				Return
			End If
			If tree.GetAllCheckedNodes().Count = 0 Then
				Interactivity.ClearMasterFilter()
			Else
				Dim flatDataRows = tree.GetAllCheckedNodes().Select(Function(node) tree.GetDataRecordByNode(node)).Cast(Of DashboardFlatDataSourceRow)()
				Interactivity.SetMasterFilter(flatDataRows)
			End If
		End Sub
		Private Sub Tree_BeforeCheckNode(ByVal sender As Object, ByVal e As CheckNodeEventArgs)

			If tree.OptionsView.CheckBoxStyle = DefaultNodeCheckBoxStyle.Radio Then
				skipMasterFiltering = True
				tree.UncheckAll()
				skipMasterFiltering = False
			End If
		End Sub
		Protected Overrides Sub UpdateControl(ByVal customItemData As CustomItemData)
			skipMasterFiltering = True
			tree.DataSource = Nothing
			tree.KeyFieldName = String.Empty
			tree.ParentFieldName = tree.KeyFieldName
			If Not ValidateBindings() Then
				Return
			End If
			SetDataBindings(customItemData)
			SetSelectionMode()
			skipMasterFiltering = False
		End Sub
		Private Sub SetDataBindings(ByVal customItemData As CustomItemData)
			Try
				flatData = customItemData.GetFlatData()
				tree.DataSource = flatData
				tree.PopulateColumns()
				tree.ParentFieldName = dashboardItem.Metadata.ParentID.UniqueId
				tree.KeyFieldName = dashboardItem.Metadata.ID.UniqueId
				tree.ExpandAll()
			Catch
				flatData = Nothing
				tree.DataSource = flatData
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

			If (dashboardItem.Metadata.ID.TopNOptions.Enabled AndAlso dashboardItem.Metadata.ID.TopNOptions.ShowOthers) OrElse (dashboardItem.Metadata.ParentID.TopNOptions.Enabled AndAlso dashboardItem.Metadata.ParentID.TopNOptions.ShowOthers) Then
				validationError = "The Tree List Item does not support the ""Show Others value"" option in the ID and Parent ID data sections"
				Return False
			End If
			If dashboardItem.InteractivityOptions.IsDrillDownEnabled Then
				validationError = "The Tree List Item does not support Drill-Down"
				Return False
			End If
			If dashboardItem.Metadata.Dimensions.Count = 0 Then
				Return False
			End If
			Return True
		End Function
		Private Sub SetSelectionMode()
			Select Case dashboardItem.InteractivityOptions.MasterFilterMode
				Case DashboardItemMasterFilterMode.None
					tree.OptionsView.CheckBoxStyle = DefaultNodeCheckBoxStyle.Default
					Return
				Case DashboardItemMasterFilterMode.Multiple
					tree.OptionsView.CheckBoxStyle = DefaultNodeCheckBoxStyle.Check
				Case DashboardItemMasterFilterMode.Single
					tree.OptionsView.CheckBoxStyle = DefaultNodeCheckBoxStyle.Radio
			End Select
		End Sub
		Protected Overrides Function GetPrintableControl(ByVal customItemData As CustomItemData, ByVal exportInfo As CustomItemExportInfo) As XRControl
			Dim bitmap As New Bitmap(tree.Width, tree.Height)
			tree.DrawToBitmap(bitmap, tree.Bounds)
			Dim xRPictureBox As New XRPictureBox()
			xRPictureBox.ImageSource = New ImageSource(bitmap)
			Return xRPictureBox
		End Function
		Protected Overrides Sub SetSelection(ByVal selection As CustomItemSelection)
			skipMasterFiltering = True
			tree.UncheckAll()
			For Each item As Object() In selection.GetValues()
				Dim id As Object = item(0)
				tree.FindNodeByFieldValue(dashboardItem.Metadata.ID.UniqueId, id).CheckState = CheckState.Checked
			Next item
			skipMasterFiltering = False
		End Sub
	End Class
End Namespace
