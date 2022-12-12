Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.Ribbon

Namespace DevExpresss.DashboardWin.CustomItemExtension
	Public Class SankeyItemExtensionModule
		Implements IExtensionModule

		Private dashboardControl As IDashboardControl
		Public Sub AttachViewer(ByVal viewer As DashboardViewer) Implements IExtensionModule.AttachViewer
			AttachDashboardControl(viewer)
		End Sub
		Public Sub DetachViewer() Implements IExtensionModule.DetachViewer
			Detach()
		End Sub
		Public Sub AttachDesigner(ByVal designer As DashboardDesigner) Implements IExtensionModule.AttachDesigner
			AttachDashboardControl(designer)
			designer.CreateCustomItemBars(GetType(SankeyItemMetadata))
			RemoveDrillDownBarItem(designer)
		End Sub
		Public Sub DetachDesigner() Implements IExtensionModule.DetachDesigner
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
			If e.MetadataType Is GetType(SankeyItemMetadata) Then
				e.CustomControlProvider = New SankeyItemControlProvider(TryCast(dashboardControl.Dashboard.Items(e.DashboardItemName), CustomDashboardItem(Of SankeyItemMetadata)))
			End If
		End Sub
		Private Sub RemoveDrillDownBarItem(ByVal designer As DashboardDesigner)
			Dim page As RibbonPage = designer.Ribbon.GetDashboardRibbonPage(GetType(SankeyItemMetadata), DashboardRibbonPage.Data)
			Dim interactivityGroup As RibbonPageGroup = page.Groups(1)
			Dim drillDownBarItem As BarItem = interactivityGroup.ItemLinks(2).Item
			interactivityGroup.ItemLinks.Remove(drillDownBarItem)
		End Sub
	End Class
End Namespace
