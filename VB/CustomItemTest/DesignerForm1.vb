Imports DevExpress.DashboardCommon
Imports DevExpress.XtraBars.Navigation
Imports DevExpresss.DashboardWin.CustomItemExtension
Imports System.IO
Imports System.Linq

Namespace CustomItemTest
	Partial Public Class DesignerForm1
		Inherits DevExpress.XtraBars.Ribbon.RibbonForm

		Private sunburstItemModule As New SunburstItemExtensionModule()
		Private funnelItemModule As New FunnelItemExtensionModule()
		Private sankeyItemModule As New SankeyItemExtensionModule()
		Private waypointMapItemModule As New WaypointMapItemExtensionModule("YOUR BING KEY")
		Private hierarchyTreeItemModule As New TreeListItemExtensionModule()
		Private ganttItemExtensionModule As New GanttItemExtensionModule()
		Private webPageItemExtensionModule As New WebPageItemExtensionModule()
		Public Sub New()
			Dashboard.CustomItemMetadataTypes.Register(Of SunburstItemMetadata)()
			Dashboard.CustomItemMetadataTypes.Register(Of SankeyItemMetadata)()
			Dashboard.CustomItemMetadataTypes.Register(Of WaypointMapItemMetadata)()
			Dashboard.CustomItemMetadataTypes.Register(Of FunnelItemMetadata)()
			Dashboard.CustomItemMetadataTypes.Register(Of TreeListItemMetadata)()
			Dashboard.CustomItemMetadataTypes.Register(Of GanttItemMetadata)()
			Dashboard.CustomItemMetadataTypes.Register(Of WebPageItemMetadata)()

			InitializeComponent()
			dashboardDesigner.CreateRibbon()
			AttachModules()
			GenerateAccordionElements()
			dashboardsAccordion.SelectedElement = dashboardsAccordion.Elements.Last()
			dashboardsAccordion.ScrollBarMode = ScrollBarMode.Auto
		End Sub

		Private Sub AttachModules()
			sunburstItemModule.AttachDesigner(dashboardDesigner)
			funnelItemModule.AttachDesigner(dashboardDesigner)
			sankeyItemModule.AttachDesigner(dashboardDesigner)
			waypointMapItemModule.AttachDesigner(dashboardDesigner)
			hierarchyTreeItemModule.AttachDesigner(dashboardDesigner)
			ganttItemExtensionModule.AttachDesigner(dashboardDesigner)
			webPageItemExtensionModule.AttachDesigner(dashboardDesigner)
		End Sub
		Private Sub GenerateAccordionElements()
			For Each dashboardFile As String In Directory.GetFiles("Dashboards", "*.xml", SearchOption.TopDirectoryOnly)
				Dim item As AccordionControlElement = dashboardsAccordion.AddItem()
				item.Text = dashboardFile.Replace("Dashboards\","").Replace(".xml","")
				item.Tag = dashboardFile
			Next dashboardFile

		End Sub

		Private Sub dashboardsAccordion_SelectedElementChanged(ByVal sender As Object, ByVal e As SelectedElementChangedEventArgs) Handles dashboardsAccordion.SelectedElementChanged
			dashboardDesigner.LoadDashboard(e.Element.Tag.ToString())
		End Sub
	End Class
End Namespace
