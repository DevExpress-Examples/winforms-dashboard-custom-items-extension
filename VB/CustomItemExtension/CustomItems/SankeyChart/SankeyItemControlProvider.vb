Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardCommon.ViewerData
Imports DevExpress.DashboardWin
Imports DevExpress.DataProcessing
Imports DevExpress.Utils
Imports DevExpress.Utils.Extensions
Imports DevExpress.XtraCharts.Sankey
Imports DevExpress.XtraReports.UI

Namespace DevExpresss.DashboardWin.CustomItemExtension
	Public Class SankeyItemControlProvider
		Inherits CustomControlProviderBase

		Private flatData As DashboardFlatDataSource
		Private multiDimensionalData As MultiDimensionalData
		Private sankey As SankeyDiagramControl
		Private dashboardItem As CustomDashboardItem(Of SankeyItemMetadata)
		Private toolTipController As ToolTipController

		Protected Overrides ReadOnly Property Control() As Control
			Get
				Return sankey
			End Get
		End Property

		Public Sub New(ByVal dashboardItem As CustomDashboardItem(Of SankeyItemMetadata))
			Me.dashboardItem = dashboardItem
			sankey = New DashboardSankeyDiagramControl()
			sankey.BorderOptions.Thickness = 0
			sankey.EmptySankeyText.Text = String.Empty
			AddHandler sankey.SelectedItemsChanged, AddressOf Sankey_SelectedItemsChanged
			toolTipController = New ToolTipController()
			AddHandler toolTipController.BeforeShow, AddressOf ToolTipController_BeforeShow
			sankey.ToolTipController = toolTipController
			AddHandler sankey.SelectedItemsChanging, AddressOf Sankey_SelectedItemsChanging
			AddHandler sankey.HighlightedItemsChanged, AddressOf Sankey_HighlightedItemsChanged
		End Sub
		Protected Overrides Sub UpdateControl(ByVal customItemData As CustomItemData)
			multiDimensionalData = customItemData.GetMultiDimensionalData()
			flatData = customItemData.GetFlatData(New DashboardFlatDataSourceOptions() With {
				.AddColoringColumns = True,
				.AddDisplayTextColumns = True
			})
			ClearBindings()
			If ValidateBindings() Then
				SetDataBindings(flatData)
				SetSelectionMode()
			End If
		End Sub
		Protected Overrides Sub SetSelection(ByVal selection As CustomItemSelection)
			Dim selectedRows As IList(Of DashboardFlatDataSourceRow) = selection.GetDashboardFlatDataSourceRows(flatData)
			sankey.SelectedItems.Clear()
			selectedRows.ForEach(Sub(r) sankey.SelectedItems.Add(r))
		End Sub
		Protected Overrides Function GetPrintableControl(ByVal customItemData As CustomItemData, ByVal exportInfo As CustomItemExportInfo) As XRControl
			Dim container As New PrintableComponentContainer()
			container.PrintableComponent = sankey
			Return container
		End Function
		Private Sub Sankey_HighlightedItemsChanged(ByVal sender As Object, ByVal e As SankeyHighlightedItemsChangedEventArgs)
			If (sankey.SelectionMode = SankeySelectionMode.Single AndAlso e.HighlightedNodes.Count > 0) OrElse e.HighlightedLinks.Any(Function(x) HasSpecialValues(x)) Then
				sankey.HighlightedItems.Clear()
			End If
		End Sub
		Private Sub Sankey_SelectedItemsChanging(ByVal sender As Object, ByVal e As SankeySelectedItemsChangingEventArgs)
			If e.Action = DevExpress.XtraCharts.SelectedItemsChangedAction.Reset Then
				e.Cancel = True
				Return
			End If
			If (sankey.SelectionMode = SankeySelectionMode.Single AndAlso e.NewNodes.Count > 0) OrElse e.NewLinks.Any(Function(x) HasSpecialValues(x)) Then
				e.Cancel = True
			End If
		End Sub
		Private Sub ToolTipController_BeforeShow(ByVal sender As Object, ByVal e As DevExpress.Utils.ToolTipControllerShowEventArgs)
			If dashboardItem.Metadata.Weight Is Nothing Then
				e.ToolTip = String.Empty
			ElseIf TypeOf e.SelectedObject Is SankeyLink Then
				Dim link As SankeyLink = TryCast(e.SelectedObject, SankeyLink)
				e.ToolTip = multiDimensionalData.GetMeasures()(0).Format(link.TotalWeight)
			ElseIf TypeOf e.SelectedObject Is SankeyNode Then
				Dim node As SankeyNode = TryCast(e.SelectedObject, SankeyNode)
				e.ToolTip = multiDimensionalData.GetMeasures()(0).Format(node.TotalWeight)
			End If
		End Sub
		Private Sub Sankey_SelectedItemsChanged(ByVal sender As Object, ByVal e As SankeySelectedItemsChangedEventArgs)
			If sankey.SelectedItems.Count = 0 AndAlso Interactivity.CanClearMasterFilter Then
				Interactivity.ClearMasterFilter()
			ElseIf sankey.SelectedItems.Count > 0 AndAlso Interactivity.CanSetMasterFilter Then
				Interactivity.SetMasterFilter(sankey.SelectedItems.OfType(Of DashboardFlatDataSourceRow)())
			End If
		End Sub
		Private Function ValidateBindings() As Boolean
			If Interactivity.IsDrillDownEnabled Then
				sankey.EmptySankeyText.Text = "The Sankey Item does not support Drill-Down"
				Return False
			End If
			If dashboardItem.Metadata.Source Is Nothing OrElse dashboardItem.Metadata.Target Is Nothing Then
				sankey.EmptySankeyText.Text = "Add the Source and Target dimensions"
				Return False
			End If
			If dashboardItem.Metadata.Source.GetDefinition().Equals(dashboardItem.Metadata.Target.GetDefinition()) Then
				sankey.EmptySankeyText.Text = "Add different data fields to the Source and Target sections"
				Return False
			End If
			Return True
		End Function
		Private Sub SetDataBindings(ByVal flatData As DashboardFlatDataSource)
			sankey.Colorizer = New SankeyItemColorizer(flatData)
			sankey.SourceDataMember = flatData.GetDisplayTextColumn(dashboardItem.Metadata.Source.UniqueId).Name
			sankey.TargetDataMember = flatData.GetDisplayTextColumn(dashboardItem.Metadata.Target.UniqueId).Name
			If dashboardItem.Metadata.Weight IsNot Nothing Then
				sankey.WeightDataMember = dashboardItem.Metadata.Weight.UniqueId
			End If
			Try
				sankey.DataSource = flatData
			Catch
				sankey.DataSource = Nothing
			End Try
		End Sub
		Private Sub ClearBindings()
			sankey.DataSource = Nothing
			sankey.Colorizer = Nothing
			sankey.SourceDataMember = Nothing
			sankey.TargetDataMember = Nothing
			sankey.WeightDataMember = Nothing
		End Sub
		Private Sub SetSelectionMode()
			Select Case Interactivity.MasterFilterMode
				Case DashboardItemMasterFilterMode.None
					sankey.SelectionMode = SankeySelectionMode.None
					Return
				Case DashboardItemMasterFilterMode.Multiple
					sankey.SelectionMode = SankeySelectionMode.Extended
				Case DashboardItemMasterFilterMode.Single
					sankey.SelectionMode = SankeySelectionMode.Single
			End Select
		End Sub
		Private Function HasSpecialValues(ByVal link As SankeyLink) As Boolean
			Dim row As DashboardFlatDataSourceRow = DirectCast(link.Tags(0), DashboardFlatDataSourceRow)
			Return SpecialValues.IsOthersValue(flatData.GetValue(dashboardItem.Metadata.Source.UniqueId, row)) OrElse SpecialValues.IsOthersValue(flatData.GetValue(dashboardItem.Metadata.Target.UniqueId, row))
		End Function
	End Class
	Friend Class SankeyItemColorizer
		Implements ISankeyColorizer

		Private ReadOnly nodeDefaultColor As Color = Color.FromArgb(255, 95, 139, 149)
		Private ReadOnly flatData As DashboardFlatDataSource

		Public Sub New(ByVal flatData As DashboardFlatDataSource)
			Me.flatData = flatData
		End Sub
		Public Function GetLinkSourceColor(ByVal link As SankeyLink) As Color Implements ISankeyColorizer.GetLinkSourceColor
			Return GetLinkColorBase(link)
		End Function
		Public Function GetLinkTargetColor(ByVal link As SankeyLink) As Color Implements ISankeyColorizer.GetLinkTargetColor
			Return GetLinkColorBase(link)
		End Function
		Public Function GetLinkColorBase(ByVal link As SankeyLink) As Color
			Dim row As DashboardFlatDataSourceRow = TryCast(link.Tags(0), DashboardFlatDataSourceRow)
			Dim colorData As Integer = DirectCast(flatData.GetValue(flatData.GetColoringColumn().Name, row), Integer)
			Return Color.FromArgb(colorData)
		End Function
		Public Function GetNodeColor(ByVal info As SankeyNode) As Color Implements ISankeyColorizer.GetNodeColor
			Return nodeDefaultColor
		End Function
	End Class
	Public Class DashboardSankeyDiagramControl
		Inherits SankeyDiagramControl

		Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
			If e.Button <> MouseButtons.Right Then
				MyBase.OnMouseUp(e)
			End If
		End Sub
	End Class
End Namespace
