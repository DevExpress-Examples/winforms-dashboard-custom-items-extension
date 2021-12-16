Imports DevExpress.DashboardCommon
Imports System
Imports System.ComponentModel

Namespace DevExpresss.DashboardWin.CustomItemExtension
	<DisplayName("Web Page"), CustomItemDescription("Create a Web Page dashboard item and insert it into the dashboard." & vbLf & vbLf & "This item displays the web content. You can specify the URI pattern to create the page address from a data column at run-time."), CustomItemImage("WebPageItem.svg")>
	Public Class WebPageItemMetadata
		Inherits CustomItemMetadata

		<DisplayName("Attribute"), EmptyDataItemPlaceholder("Attribute")>
		Public Property Attribute() As Dimension
			Get
				Return GetPropertyValue(Of Dimension)()
			End Get
			Set(ByVal value As Dimension)
				SetPropertyValue(value)
			End Set
		End Property
		<UrlCustom>
		Public Property URI() As String
			Get
				Return GetCustomPropertyValue(Of String)()
			End Get
			Set(ByVal value As String)
				SetCustomPropertyValue(value)
			End Set
		End Property

		Public Class UrlCustomAttribute
			Inherits Attribute
			Implements ICustomPropertyValueConverterAttribute(Of String), ICustomPropertyHistoryAttribute(Of String)

			Public Function ConvertToString(ByVal value As String) As String Implements ICustomPropertyValueConverterAttribute(Of String).ConvertToString
				Return value
			End Function
			Public Function ConvertFromString(ByVal strValue As String) As String Implements ICustomPropertyValueConverterAttribute(Of String).ConvertFromString
				Return strValue
			End Function
			Public Function GetHistoryMessage(ByVal newValue As String, ByVal oldValue As String, ByVal dashboardItemName As String) As String Implements ICustomPropertyHistoryAttribute(Of String).GetHistoryMessage
				Return "URI for the " & dashboardItemName & " changed"
			End Function
		End Class
	End Class
End Namespace
