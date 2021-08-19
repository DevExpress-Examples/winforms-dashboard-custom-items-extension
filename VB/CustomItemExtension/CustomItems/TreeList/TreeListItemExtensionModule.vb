Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.Ribbon

Namespace DevExpresss.DashboardWin.CustomItemExtension
	Public Class TreeListItemExtensionModule
		Private dashboardControl As IDashboardControl
		Public Sub AttachViewer(ByVal viewer As DashboardViewer)
			AttachDashboardControl(viewer)
		End Sub
		Public Sub DetachViewer()
			Detach()
		End Sub
		Public Sub AttachDesigner(ByVal designer As DashboardDesigner)
			AttachDashboardControl(designer)
			designer.CreateCustomItemBars(GetType(TreeListItemMetadata))
			RemoveDrillDownBarItem(designer)
		End Sub
		Public Sub DetachDesigner()
			Detach()
		End Sub
		Private Sub Detach()
			If dashboardControl IsNot Nothing Then
				RemoveHandler dashboardControl.CustomDashboardItemControlCreating, AddressOf OnCustomDashboardItemControlCreating
			End If
		End Sub
		Private Sub AttachDashboardControl(ByVal dashboardControl As IDashboardControl)
			If dashboardControl IsNot Nothing Then
				Me.dashboardControl = dashboardControl
				dashboardControl.CalculateHiddenTotals = True
				AddHandler dashboardControl.CustomDashboardItemControlCreating, AddressOf OnCustomDashboardItemControlCreating
			End If
		End Sub
		Private Sub OnCustomDashboardItemControlCreating(ByVal sender As Object, ByVal e As CustomDashboardItemControlCreatingEventArgs)
			Dim dashboardControl As IDashboardControl = DirectCast(sender, IDashboardControl)
			If e.MetadataType Is GetType(TreeListItemMetadata) Then
				e.CustomControlProvider = New TreeListItemControlProvider(TryCast(dashboardControl.Dashboard.Items(e.DashboardItemName), CustomDashboardItem(Of TreeListItemMetadata)))
			End If
		End Sub
		Private Sub RemoveDrillDownBarItem(ByVal designer As DashboardDesigner)
			Dim page As RibbonPage = designer.Ribbon.GetDashboardRibbonPage(GetType(TreeListItemMetadata), DashboardRibbonPage.Data)
			Dim interactivityGroup As RibbonPageGroup = page.Groups(1)
			Dim drillDownBarItem As BarItem = interactivityGroup.ItemLinks(2).Item
			interactivityGroup.ItemLinks.Remove(drillDownBarItem)
		End Sub

	End Class
End Namespace
