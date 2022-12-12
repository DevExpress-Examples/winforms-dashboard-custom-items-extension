Imports DevExpress.DashboardCommon
Imports Newtonsoft.Json
Imports System
Imports System.ComponentModel

Namespace DevExpresss.DashboardWin.CustomItemExtension
    <DisplayName("Heatmap"), CustomItemDescription("Create a Heatmap dashboard item and insert it into the dashboard." & vbLf & vbLf & " The Heatmap item uses color variation to show the relationship between two dimensions."), CustomItemImage("HeatmapCustomItem.svg")>
    Public Class HeatmapItemMetadata
        Inherits CustomItemMetadata

        Private ReadOnly rows As New DimensionCollection()
        Private ReadOnly columns As New DimensionCollection()
        <DisplayName("Value"), EmptyDataItemPlaceholder("Value")>
        Public Property Value() As Measure
            Get
                Return GetPropertyValue(Of Measure)()
            End Get
            Set(ByVal value As Measure)
                SetPropertyValue(value)
            End Set
        End Property
        <DisplayName("Row"), EmptyDataItemPlaceholder("Row"), SupportInteractivity>
        Public Property Row() As Dimension
            Get
                Return GetPropertyValue(Of Dimension)()
            End Get
            Set(ByVal value As Dimension)
                SetPropertyValue(value)
            End Set
        End Property
        <DisplayName("Column"), EmptyDataItemPlaceholder("Column"), SupportInteractivity>
        Public Property Column() As Dimension
            Get
                Return GetPropertyValue(Of Dimension)()
            End Get
            Set(ByVal value As Dimension)
                SetPropertyValue(value)
            End Set
        End Property
        <MapPaletteCustom>
        Public Property Palette() As MapPalette
            Get
                Return GetCustomPropertyValue(Of MapPalette)()
            End Get
            Set(ByVal value As MapPalette)
                SetCustomPropertyValue(value)
            End Set
        End Property
        <MapScaleCustom>
        Public Property Scale() As MapScale
            Get
                Return GetCustomPropertyValue(Of MapScale)()
            End Get
            Set(ByVal value As MapScale)
                SetCustomPropertyValue(value)
            End Set
        End Property
        <ShowLabelsCustom>
        Public Property ShowLabels() As Boolean
            Get
                Return GetCustomPropertyValue(Of Boolean)()
            End Get
            Set(ByVal value As Boolean)
                SetCustomPropertyValue(value)
            End Set
        End Property
        <ShowLegendCustom>
        Public Property ShowLegend() As Boolean
            Get
                Return GetCustomPropertyValue(Of Boolean)()
            End Get
            Set(ByVal value As Boolean)
                SetCustomPropertyValue(value)
            End Set
        End Property
        <EnableZoomingXCustom>
        Public Property EnableZoomingXAxis() As Boolean
            Get
                Return GetCustomPropertyValue(Of Boolean)()
            End Get
            Set(ByVal value As Boolean)
                SetCustomPropertyValue(value)
            End Set
        End Property
        <EnableZoomingYCustom>
        Public Property EnableZoomingYAxis() As Boolean
            Get
                Return GetCustomPropertyValue(Of Boolean)()
            End Get
            Set(ByVal value As Boolean)
                SetCustomPropertyValue(value)
            End Set
        End Property
    End Class
    Public Class MapPaletteCustomAttribute
        Inherits Attribute
        Implements ICustomPropertyValueConverterAttribute(Of MapPalette), ICustomPropertyHistoryAttribute(Of MapPalette)

        Public Function ConvertToString(ByVal mapPalette As MapPalette) As String Implements ICustomPropertyValueConverterAttribute(Of MapPalette).ConvertToString
            If TypeOf mapPalette Is CustomHeatmapPalette Then
                Return (TryCast(mapPalette, CustomHeatmapPalette)).ToJson()
            End If
            If TypeOf mapPalette Is GradientHeatmapPalette Then
                Return (TryCast(mapPalette, GradientHeatmapPalette)).ToJson()
            End If
            Return String.Empty
        End Function
        Public Function ConvertFromString(ByVal strValue As String) As MapPalette Implements ICustomPropertyValueConverterAttribute(Of MapPalette).ConvertFromString
            Dim customPalette As CustomHeatmapPalette = CustomHeatmapPalette.FromJson(strValue)
            If customPalette IsNot Nothing Then
                Return customPalette
            End If
            Dim gradientPalette As GradientHeatmapPalette = GradientHeatmapPalette.FromJson(strValue)
            If gradientPalette IsNot Nothing Then
                Return gradientPalette
            End If
            Return Nothing
        End Function
        Public Function GetHistoryMessage(ByVal newValue As MapPalette, ByVal oldValue As MapPalette, ByVal dashboardItemName As String) As String Implements ICustomPropertyHistoryAttribute(Of MapPalette).GetHistoryMessage
            Return "Palette for the " & dashboardItemName & " changed"
        End Function
    End Class
    Public Class MapScaleCustomAttribute
        Inherits Attribute
        Implements ICustomPropertyValueConverterAttribute(Of MapScale), ICustomPropertyHistoryAttribute(Of MapScale)

        Public Function ConvertToString(ByVal mapScale As MapScale) As String Implements ICustomPropertyValueConverterAttribute(Of MapScale).ConvertToString
            If TypeOf mapScale Is CustomHeatmapScale Then
                Return (TryCast(mapScale, CustomHeatmapScale)).ToJson()
            End If
            If TypeOf mapScale Is UniformHeatmapScale Then
                Return (TryCast(mapScale, UniformHeatmapScale)).ToJson()
            End If
            Return String.Empty
        End Function
        Public Function ConvertFromString(ByVal strValue As String) As MapScale Implements ICustomPropertyValueConverterAttribute(Of MapScale).ConvertFromString
            Dim customScale As CustomHeatmapScale = CustomHeatmapScale.FromJson(strValue)
            If customScale IsNot Nothing Then
                Return customScale
            End If
            Dim uniformScale As UniformHeatmapScale = UniformHeatmapScale.FromJson(strValue)
            If uniformScale IsNot Nothing Then
                Return uniformScale
            End If
            Return Nothing
        End Function
        Public Function GetHistoryMessage(ByVal newValue As MapScale, ByVal oldValue As MapScale, ByVal dashboardItemName As String) As String Implements ICustomPropertyHistoryAttribute(Of MapScale).GetHistoryMessage
            Return "Scale for the " & dashboardItemName & " changed"
        End Function
    End Class
    Public Class CustomHeatmapScale
        Inherits CustomScale

        Public Function ToJson() As String
            Return JsonConvert.SerializeObject(Me)
        End Function
        Public Shared Function FromJson(ByVal json As String) As CustomHeatmapScale
            Try
                Return JsonConvert.DeserializeObject(Of CustomHeatmapScale)(json, New JsonSerializerSettings() With {.MissingMemberHandling = MissingMemberHandling.Error})
            Catch
                Return Nothing
            End Try
        End Function
    End Class
    Public Class UniformHeatmapScale
        Inherits UniformScale

        Public Function ToJson() As String
            Return JsonConvert.SerializeObject(Me)
        End Function
        Public Shared Function FromJson(ByVal json As String) As UniformHeatmapScale
            Try
                Return JsonConvert.DeserializeObject(Of UniformHeatmapScale)(json, New JsonSerializerSettings() With {.MissingMemberHandling = MissingMemberHandling.Error})
            Catch
                Return Nothing
            End Try
        End Function
    End Class
    Public Class CustomHeatmapPalette
        Inherits CustomPalette

        Public Function ToJson() As String
            Return JsonConvert.SerializeObject(Me)
        End Function
        Public Shared Function FromJson(ByVal json As String) As CustomHeatmapPalette
            Try
                Return JsonConvert.DeserializeObject(Of CustomHeatmapPalette)(json, New JsonSerializerSettings() With {.MissingMemberHandling = MissingMemberHandling.Error})
            Catch
                Return Nothing
            End Try
        End Function
    End Class
    Public Class GradientHeatmapPalette
        Inherits GradientPalette

        Public Function ToJson() As String
            Return JsonConvert.SerializeObject(Me)
        End Function
        Public Shared Function FromJson(ByVal json As String) As GradientHeatmapPalette
            Try
                Return JsonConvert.DeserializeObject(Of GradientHeatmapPalette)(json, New JsonSerializerSettings() With {.MissingMemberHandling = MissingMemberHandling.Error})
            Catch
                Return Nothing
            End Try
        End Function
    End Class
    Public Class ShowLabelsCustomAttribute
        Inherits Attribute
        Implements ICustomPropertyValueConverterAttribute(Of Boolean), ICustomPropertyHistoryAttribute(Of Boolean)

        Public Function ConvertToString(ByVal value As Boolean) As String Implements ICustomPropertyValueConverterAttribute(Of Boolean).ConvertToString
            Return value.ToString()
        End Function
        Public Function ConvertFromString(ByVal strValue As String) As Boolean Implements ICustomPropertyValueConverterAttribute(Of Boolean).ConvertFromString
            Dim result As Boolean = Nothing
            Boolean.TryParse(strValue, result)
            Return result
        End Function
        Public Function GetHistoryMessage(ByVal newValue As Boolean, ByVal oldValue As Boolean, ByVal dashboardItemName As String) As String Implements ICustomPropertyHistoryAttribute(Of Boolean).GetHistoryMessage
            Return (If(newValue, "Enable", "Disable")) & " Show labels for the " & dashboardItemName
        End Function
    End Class
    Public Class ShowLegendCustomAttribute
        Inherits Attribute
        Implements ICustomPropertyValueConverterAttribute(Of Boolean), ICustomPropertyHistoryAttribute(Of Boolean)

        Public Function ConvertToString(ByVal value As Boolean) As String Implements ICustomPropertyValueConverterAttribute(Of Boolean).ConvertToString
            Return value.ToString()
        End Function
        Public Function ConvertFromString(ByVal strValue As String) As Boolean Implements ICustomPropertyValueConverterAttribute(Of Boolean).ConvertFromString
            Dim result As Boolean = Nothing
            Boolean.TryParse(strValue, result)
            Return result
        End Function
        Public Function GetHistoryMessage(ByVal newValue As Boolean, ByVal oldValue As Boolean, ByVal dashboardItemName As String) As String Implements ICustomPropertyHistoryAttribute(Of Boolean).GetHistoryMessage
            Return (If(newValue, "Enable", "Disable")) & " legend visibility for " & dashboardItemName
        End Function
    End Class
    Public Class EnableZoomingXCustomAttribute
        Inherits Attribute
        Implements ICustomPropertyValueConverterAttribute(Of Boolean), ICustomPropertyHistoryAttribute(Of Boolean)

        Public Function ConvertToString(ByVal value As Boolean) As String Implements ICustomPropertyValueConverterAttribute(Of Boolean).ConvertToString
            Return value.ToString()
        End Function
        Public Function ConvertFromString(ByVal strValue As String) As Boolean Implements ICustomPropertyValueConverterAttribute(Of Boolean).ConvertFromString
            Dim result As Boolean = Nothing
            Boolean.TryParse(strValue, result)
            Return result
        End Function
        Public Function GetHistoryMessage(ByVal newValue As Boolean, ByVal oldValue As Boolean, ByVal dashboardItemName As String) As String Implements ICustomPropertyHistoryAttribute(Of Boolean).GetHistoryMessage
            Return (If(newValue, "Enable", "Disable")) & " X-axis scaling for " & dashboardItemName
        End Function
    End Class
    Public Class EnableZoomingYCustomAttribute
        Inherits Attribute
        Implements ICustomPropertyValueConverterAttribute(Of Boolean), ICustomPropertyHistoryAttribute(Of Boolean)

        Public Function ConvertToString(ByVal value As Boolean) As String Implements ICustomPropertyValueConverterAttribute(Of Boolean).ConvertToString
            Return value.ToString()
        End Function
        Public Function ConvertFromString(ByVal strValue As String) As Boolean Implements ICustomPropertyValueConverterAttribute(Of Boolean).ConvertFromString
            Dim result As Boolean = Nothing
            Boolean.TryParse(strValue, result)
            Return result
        End Function
        Public Function GetHistoryMessage(ByVal newValue As Boolean, ByVal oldValue As Boolean, ByVal dashboardItemName As String) As String Implements ICustomPropertyHistoryAttribute(Of Boolean).GetHistoryMessage
            Return (If(newValue, "Enable", "Disable")) & " Y-axis scaling for " & dashboardItemName
        End Function
    End Class
End Namespace
