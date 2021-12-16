Imports DevExpress.DashboardCommon
Imports System.ComponentModel

Namespace DevExpresss.DashboardWin.CustomItemExtension
	<DisplayName("Tree List"), CustomItemDescription("Create a Tree List dashboard item and insert it into the dashboard." & vbLf & vbLf & "This hybrid item combines a Tree List and Grid. The Tree List uses the parent-child relationships to generate the hierarchical data structure."), CustomItemImage("TreeListItem.svg")>
	Public Class TreeListItemMetadata
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
		<DisplayName("Dimensions"), EmptyDataItemPlaceholder("Dimension"), SupportInteractivity>
		Public ReadOnly Property Dimensions() As New DimensionCollection()
		<DisplayName("Measures"), EmptyDataItemPlaceholder("Measure")>
		Public ReadOnly Property Measures() As New MeasureCollection()


	End Class

End Namespace
