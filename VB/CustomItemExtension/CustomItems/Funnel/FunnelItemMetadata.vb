Imports System
Imports System.ComponentModel
Imports DevExpress.DashboardCommon
Imports DevExpress.XtraCharts

Namespace DevExpresss.DashboardWin.CustomItemExtension
	<DisplayName("Funnel"), CustomItemDescription("Create a Funnel chart dashboard item and insert it into the dashboard." & vbLf & vbLf & "The Funnel chart visualizes the progressive reduction of data as it passes from one stage to another in a process or procedure."), CustomItemImage("DevExpresss.DashboardWin.CustomItemExtension.Images.FunnelItem.svg")>
	Public Class FunnelItemMetadata
		Inherits CustomItemMetadata

		<DisplayName("Value"), EmptyDataItemPlaceholder("Value"), SupportColoring(DefaultColoringMode.None)>
		Public Property Value() As Measure
			Get
				Return GetPropertyValue(Of Measure)()
			End Get
			Set(ByVal value As Measure)
				SetPropertyValue(value)
			End Set
		End Property
		<DisplayName("Arguments"), EmptyDataItemPlaceholder("Argument"), SupportColoring(DefaultColoringMode.Hue), SupportInteractivity>
		Public ReadOnly Property Arguments() As New DimensionCollection()

	End Class
End Namespace
