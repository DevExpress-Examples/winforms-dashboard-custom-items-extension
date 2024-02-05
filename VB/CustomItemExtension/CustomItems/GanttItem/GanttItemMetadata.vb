Imports DevExpress.DashboardCommon
Imports System.ComponentModel

Namespace DevExpress.DashboardWin.CustomItemExtension
	<DisplayName("Gantt"), CustomItemDescription("Create a Gantt dashboard item and insert it into the dashboard." & vbLf & vbLf & "The Gantt item dispalys tasks organized in a tree list with a bar chart that visualizes a schedule data."), CustomItemImage("GanttItem.svg")>
	Public Class GanttItemMetadata
		Inherits CustomItemMetadata

		<DisplayName("ID"), EmptyDataItemPlaceholder("ID"), SupportInteractivity>
		Public Property ID() As Dimension
			Get
				Return GetPropertyValue(Of Dimension)()
			End Get
			Set(ByVal value As Dimension)
				SetPropertyValue(value)
			End Set
		End Property

		<DisplayName("Parent ID"), EmptyDataItemPlaceholder("Parent ID"), SupportInteractivity>
		Public Property ParentID() As Dimension
			Get
				Return GetPropertyValue(Of Dimension)()
			End Get
			Set(ByVal value As Dimension)
				SetPropertyValue(value)
			End Set
		End Property

		<DisplayName("Text"), EmptyDataItemPlaceholder("Text"), SupportColoring(DefaultColoringMode.None), SupportInteractivity>
		Public Property Text() As Dimension
			Get
				Return GetPropertyValue(Of Dimension)()
			End Get
			Set(ByVal value As Dimension)
				SetPropertyValue(value)
			End Set
		End Property

		<DisplayName("Start Date"), EmptyDataItemPlaceholder("Start Date"), SupportedDataTypes(DataSourceFieldType.DateTime), DefaultGroupInterval(DateTimeGroupInterval.DayMonthYear), SupportInteractivity>
		Public Property StartDate() As Dimension
			Get
				Return GetPropertyValue(Of Dimension)()
			End Get
			Set(ByVal value As Dimension)
				SetPropertyValue(value)
			End Set
		End Property

		<DisplayName("Finish Date"), EmptyDataItemPlaceholder("Finish Date"), SupportedDataTypes(DataSourceFieldType.DateTime), DefaultGroupInterval(DateTimeGroupInterval.DayMonthYear), SupportInteractivity>
		Public Property FinishDate() As Dimension
			Get
				Return GetPropertyValue(Of Dimension)()
			End Get
			Set(ByVal value As Dimension)
				SetPropertyValue(value)
			End Set
		End Property
	End Class
End Namespace
