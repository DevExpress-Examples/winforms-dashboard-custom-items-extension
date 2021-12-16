Imports DevExpress.DashboardCommon
Imports System.ComponentModel

Namespace DevExpresss.DashboardWin.CustomItemExtension
	<DisplayName("Sunburst"), CustomItemDescription("Create a Sunburst dashboard item and insert it into the dashboard." & vbLf & vbLf & "The Sunburst combines a TreeMap and a Pie chart to visualize hierarchical data in a circular layout."), CustomItemImage("SunburstCustomItem.svg")>
	Public Class SunburstItemMetadata
		Inherits CustomItemMetadata

'INSTANT VB NOTE: The field arguments was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private ReadOnly arguments_Renamed As New DimensionCollection()
		<DisplayName("Value"), EmptyDataItemPlaceholder("Value")>
		Public Property Value() As Measure
			Get
				Return GetPropertyValue(Of Measure)()
			End Get
			Set(ByVal value As Measure)
				SetPropertyValue(value)
			End Set
		End Property
		<DisplayName("Arguments"), EmptyDataItemPlaceholder("Argument"), SupportColoring(DefaultColoringMode.None), SupportInteractivity>
		Public ReadOnly Property Arguments() As DimensionCollection
			Get
				Return arguments_Renamed
			End Get
		End Property
	End Class
End Namespace
