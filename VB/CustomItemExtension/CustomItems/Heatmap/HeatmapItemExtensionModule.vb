Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports DevExpress.DashboardWin.Native
Imports DevExpress.Utils.Svg
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.DashboardWin.CustomItemExtension.Heatmap
Imports System
Imports System.IO
Imports System.Reflection

Namespace DevExpress.DashboardWin.CustomItemExtension
	Public Class HeatmapItemExtensionModule
		Implements IExtensionModule

		Private editColorsBarItem As BarButtonItem
		Private barShowLabelsItem As BarCheckItem
		Private barShowLegendItem As BarCheckItem
		Private barEnableZoomXAxisItem As BarCheckItem
		Private barEnableZoomYAxisItem As BarCheckItem
		Private ReadOnly Property SelectedCustomItem() As CustomDashboardItem(Of HeatmapItemMetadata)
			Get
				Return TryCast(designer.SelectedDashboardItem, CustomDashboardItem(Of HeatmapItemMetadata))
			End Get
		End Property
		Private dashboardControl As IDashboardControl
		Private ReadOnly Property designer() As DashboardDesigner
			Get
				Return TryCast(dashboardControl, DashboardDesigner)
			End Get
		End Property
		Public Sub AttachViewer(ByVal viewer As DashboardViewer) Implements IExtensionModule.AttachViewer
			AttachDashboardControl(viewer)
		End Sub
		Public Sub DetachViewer() Implements IExtensionModule.DetachViewer
			Detach()
		End Sub
		Public Sub AttachDesigner(ByVal designer As DashboardDesigner) Implements IExtensionModule.AttachDesigner
			AttachDashboardControl(designer)
			designer.CreateCustomItemBars(GetType(HeatmapItemMetadata))
			AddHandler designer.DashboardCustomPropertyChanged, AddressOf UpdateBarsEventHandler
			AddHandler designer.DashboardItemSelected, AddressOf UpdateBarsEventHandler
			InitializeBarItems()
		End Sub
		Public Sub DetachDesigner() Implements IExtensionModule.DetachDesigner
			RemoveHandler designer.DashboardCustomPropertyChanged, AddressOf UpdateBarsEventHandler
			RemoveHandler designer.DashboardItemSelected, AddressOf UpdateBarsEventHandler
			Detach()
		End Sub
		Private Sub Detach()
			If dashboardControl IsNot Nothing Then
				RemoveHandler dashboardControl.CustomDashboardItemControlCreating, AddressOf OnCustomDashboardItemControlCreating
			End If
			dashboardControl = Nothing
		End Sub
		Private Sub AttachDashboardControl(ByVal dashboardControl As IDashboardControl)
			If dashboardControl IsNot Nothing Then
				Me.dashboardControl = dashboardControl
				AddHandler dashboardControl.CustomDashboardItemControlCreating, AddressOf OnCustomDashboardItemControlCreating
			End If
		End Sub
		Private Sub InitializeBarItems()
			Dim page As RibbonPage = designer.Ribbon.GetDashboardRibbonPage(GetType(HeatmapItemMetadata), DashboardRibbonPage.Design)
			Dim coloringGroup As RibbonPageGroup = page.GetGroupByName("Coloring")
			If coloringGroup Is Nothing Then
				coloringGroup = New RibbonPageGroup("Coloring") With {.Name = "Coloring"}
				page.Groups.Add(coloringGroup)
				coloringGroup.AllowTextClipping = False
			End If
			editColorsBarItem = New BarButtonItem(designer.Ribbon.Manager, "Edit Colors")
			editColorsBarItem.ImageOptions.SvgImage = SvgImage.FromResources("DevExpress.DashboardWin.CustomItemExtension.Images.Heatmap_ColorScheme.svg", Me.GetType().Assembly)
			AddHandler editColorsBarItem.ItemClick, AddressOf OptionsBarItem_ItemClick
			coloringGroup.ItemLinks.Add(editColorsBarItem)
			Dim group As RibbonPageGroup = page.GetGroupByName("Diagram")
			If group Is Nothing Then
				group = New RibbonPageGroup("Diagram") With {.Name = "Diagram"}
				page.Groups.Add(group)
				group.AllowTextClipping = False
			End If
			barShowLabelsItem = New BarCheckItem(designer.Ribbon.Manager)
			barShowLabelsItem.Checked = False
			barShowLabelsItem.ImageOptions.SvgImage = SvgImage.FromResources("DevExpress.DashboardWin.CustomItemExtension.Images.Heatmap_ShowLabels.svg", Me.GetType().Assembly)
			barShowLabelsItem.Caption = "Show Labels"
			AddHandler barShowLabelsItem.ItemClick, Sub(s, e)
														If SelectedCustomItem IsNot Nothing Then
															SelectedCustomItem.Metadata.ShowLabels = Not SelectedCustomItem.Metadata.ShowLabels
														End If
													End Sub
			barShowLegendItem = New BarCheckItem(designer.Ribbon.Manager)
			barShowLegendItem.Checked = False
			barShowLegendItem.ImageOptions.SvgImage = SvgImage.FromResources("DevExpress.DashboardWin.CustomItemExtension.Images.Heatmap_ShowLegend.svg", Me.GetType().Assembly)
			barShowLegendItem.Caption = "Show Legend"
			AddHandler barShowLegendItem.ItemClick, Sub(s, e)
														If SelectedCustomItem IsNot Nothing Then
															SelectedCustomItem.Metadata.ShowLegend = Not SelectedCustomItem.Metadata.ShowLegend
														End If
													End Sub
			barEnableZoomXAxisItem = New BarCheckItem(designer.Ribbon.Manager)
			barEnableZoomXAxisItem.Checked = False
			barEnableZoomXAxisItem.ImageOptions.SvgImage = SvgImage.FromResources("DevExpress.DashboardWin.CustomItemExtension.Images.Heatmap_ScrollHorizontal.svg", Me.GetType().Assembly)
			barEnableZoomXAxisItem.Caption = "Enable Zooming X-Axis"
			AddHandler barEnableZoomXAxisItem.ItemClick, Sub(s, e)
															 If SelectedCustomItem IsNot Nothing Then
																 SelectedCustomItem.Metadata.EnableZoomingXAxis = Not SelectedCustomItem.Metadata.EnableZoomingXAxis
															 End If
														 End Sub
			barEnableZoomYAxisItem = New BarCheckItem(designer.Ribbon.Manager)
			barEnableZoomYAxisItem.Checked = False
			barEnableZoomYAxisItem.ImageOptions.SvgImage = SvgImage.FromResources("DevExpress.DashboardWin.CustomItemExtension.Images.Heatmap_ScrollVertical.svg", Me.GetType().Assembly)
			barEnableZoomYAxisItem.Caption = "Enable Zooming Y-Axis"
			AddHandler barEnableZoomYAxisItem.ItemClick, Sub(s, e)
															 If SelectedCustomItem IsNot Nothing Then
																 SelectedCustomItem.Metadata.EnableZoomingYAxis = Not SelectedCustomItem.Metadata.EnableZoomingYAxis
															 End If
														 End Sub
			group.ItemLinks.Add(barShowLabelsItem)
			group.ItemLinks.Add(barShowLegendItem)
			group.ItemLinks.Add(barEnableZoomXAxisItem)
			group.ItemLinks.Add(barEnableZoomYAxisItem)
		End Sub
		Private Sub UpdateBarsEventHandler(ByVal sender As Object, ByVal e As EventArgs)
			If SelectedCustomItem IsNot Nothing Then
				barShowLabelsItem.Checked = SelectedCustomItem.Metadata.ShowLabels
				barShowLegendItem.Checked = SelectedCustomItem.Metadata.ShowLegend
				barEnableZoomXAxisItem.Checked = SelectedCustomItem.Metadata.EnableZoomingXAxis
				barEnableZoomYAxisItem.Checked = SelectedCustomItem.Metadata.EnableZoomingYAxis
			End If
		End Sub
		Private Sub OptionsBarItem_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
			Using dlg As New HeatmapOptionsForm(SelectedCustomItem.Metadata.Palette, SelectedCustomItem.Metadata.Scale)
				If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
					SelectedCustomItem.Metadata.Palette = dlg.GetPalette()
					SelectedCustomItem.Metadata.Scale = dlg.GetScale()
				End If
			End Using
		End Sub
		Private Sub OnCustomDashboardItemControlCreating(ByVal sender As Object, ByVal e As CustomDashboardItemControlCreatingEventArgs)
			Dim dashboardControl As IDashboardControl = DirectCast(sender, IDashboardControl)
			If e.MetadataType Is GetType(HeatmapItemMetadata) Then
				e.CustomControlProvider = New HeatmapItemControlProvider(TryCast(dashboardControl.Dashboard.Items(e.DashboardItemName), CustomDashboardItem(Of HeatmapItemMetadata)))
			End If
		End Sub
	End Class
End Namespace
