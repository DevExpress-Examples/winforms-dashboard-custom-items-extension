Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraEditors.Repository
Imports DevExpresss.DashboardWin.CustomItemExtension.CustomItems.WebPageItem

Namespace DevExpresss.DashboardWin.CustomItemExtension
	Public Class WebPageItemExtensionModule
		Implements IExtensionModule

		Private urlBarItem As BarEditItem
		Private dashboardControl As IDashboardControl
		Private ReadOnly Property designer() As DashboardDesigner
			Get
				Return TryCast(dashboardControl, DashboardDesigner)
			End Get
		End Property
		Private ReadOnly Property selectedCustomItem() As CustomDashboardItem(Of WebPageItemMetadata)
			Get
				Return TryCast(designer.SelectedDashboardItem, CustomDashboardItem(Of WebPageItemMetadata))
			End Get
		End Property
		Public Sub AttachViewer(ByVal viewer As DashboardViewer) Implements IExtensionModule.AttachViewer
			AttachDashboardControl(viewer)
		End Sub
		Public Sub AttachDesigner(ByVal designer As DashboardDesigner) Implements IExtensionModule.AttachDesigner
			AttachDashboardControl(designer)
			designer.CreateCustomItemBars(GetType(WebPageItemMetadata))
			InitializeUrlBarItem()
			AddHandler designer.DashboardItemSelected, AddressOf Designer_DashboardItemSelected
			' Other code specific for Designer
		End Sub

		Private Sub Designer_DashboardItemSelected(ByVal sender As Object, ByVal e As DashboardItemSelectedEventArgs)
			If TypeOf designer.SelectedDashboardItem Is CustomDashboardItem(Of WebPageItemMetadata) Then
				urlBarItem.EditValue = selectedCustomItem.Metadata.URI
			End If
		End Sub

		Private Sub AttachDashboardControl(ByVal dashboardControl As IDashboardControl)
			If dashboardControl IsNot Nothing Then
				Me.dashboardControl = dashboardControl
				AddHandler dashboardControl.CustomDashboardItemControlCreating, AddressOf OnCustomDashboardItemControlCreating
			End If
		End Sub
		Public Sub DetachViewer() Implements IExtensionModule.DetachViewer
			Detach()
		End Sub
		Public Sub DetachDesigner() Implements IExtensionModule.DetachDesigner
			Detach()
		End Sub
		Private Sub Detach()
			If dashboardControl IsNot Nothing Then
				RemoveHandler dashboardControl.CustomDashboardItemControlCreating, AddressOf OnCustomDashboardItemControlCreating
			End If
		End Sub
		Private Sub OnCustomDashboardItemControlCreating(ByVal sender As Object, ByVal e As CustomDashboardItemControlCreatingEventArgs)
			If e.MetadataType Is GetType(WebPageItemMetadata) Then
				e.CustomControlProvider = New WebPageItemControlProvider(TryCast(dashboardControl.Dashboard.Items(e.DashboardItemName), CustomDashboardItem(Of WebPageItemMetadata)))
			End If
		End Sub
		Private Sub InitializeUrlBarItem()
			Dim page As RibbonPage = designer.Ribbon.GetDashboardRibbonPage(GetType(WebPageItemMetadata), DashboardRibbonPage.Design)
			Dim group As RibbonPageGroup = page.GetGroupByName("URI")
			If group Is Nothing Then
				group = New RibbonPageGroup("URI") With {.Name = "URI"}
				page.Groups.Add(group)
				group.AllowTextClipping = False
			End If

			Dim buttonEdit As New RepositoryItemButtonEdit()
			buttonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
			AddHandler buttonEdit.ButtonClick, AddressOf ButtonEdit_ButtonClick
			urlBarItem = New BarEditItem(designer.Ribbon.Manager, buttonEdit)
			urlBarItem.Caption = "URI Pattern"
			urlBarItem.EditWidth = 150
			group.ItemLinks.Add(urlBarItem)
		End Sub

		Private Sub ButtonEdit_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
			Using dlg As New UriEditForm()
				dlg.UriPattern = selectedCustomItem.Metadata.URI
				If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
					selectedCustomItem.Metadata.URI = dlg.UriPattern
					urlBarItem.EditValue = dlg.UriPattern
				End If
			End Using
		End Sub
	End Class
End Namespace
