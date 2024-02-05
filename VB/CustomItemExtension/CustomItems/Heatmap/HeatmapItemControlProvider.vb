Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardCommon.Native
Imports DevExpress.DashboardCommon.ViewerData
Imports DevExpress.DashboardCommon.ViewModel
Imports DevExpress.DashboardWin
Imports DevExpress.LookAndFeel
Imports DevExpress.Skins
Imports DevExpress.Utils
Imports DevExpress.Utils.Extensions
Imports DevExpress.XtraCharts
Imports DevExpress.XtraCharts.Heatmap
Imports DevExpress.XtraMap
Imports DevExpress.XtraMap.Native
Imports DevExpress.XtraReports.UI
Imports HeatmapDataSourceAdapter = DevExpress.XtraCharts.Heatmap.HeatmapDataSourceAdapter
Imports DevExpress.XtraMap.Native.SupportSkin

Namespace DevExpress.DashboardWin.CustomItemExtension
	Public Class HeatmapItemControlProvider
		Inherits CustomControlProviderBase

		Private heatmap As HeatmapControl
		Private dataAdapter As HeatmapDataSourceAdapter
		Private dashboardItem As CustomDashboardItem(Of HeatmapItemMetadata)
		Private flatData As DashboardFlatDataSource
		Protected Overrides ReadOnly Property Control() As Control
			Get
				Return heatmap
			End Get
		End Property
		Public Sub New(ByVal dashboardItem As CustomDashboardItem(Of HeatmapItemMetadata))
			Me.dashboardItem = dashboardItem
			heatmap = New HeatmapControl()
			Me.dataAdapter = New HeatmapDataSourceAdapter()
			heatmap.ToolTipEnabled = True
		End Sub
		Protected Overrides Sub UpdateControl(ByVal customItemData As CustomItemData)
			heatmap.Label.Visible = dashboardItem.Metadata.ShowLabels
			heatmap.Legend.Visibility = If(dashboardItem.Metadata.ShowLegend, DefaultBoolean.True, DefaultBoolean.False)
			heatmap.EnableAxisXScrolling = dashboardItem.Metadata.EnableZoomingXAxis
			heatmap.EnableAxisXZooming = heatmap.EnableAxisXScrolling
			heatmap.EnableAxisYScrolling = dashboardItem.Metadata.EnableZoomingYAxis
			heatmap.EnableAxisYZooming = heatmap.EnableAxisYScrolling
			ClearDataBindings()
			If ValidateBindings() Then
				flatData = customItemData.GetFlatData(New DashboardFlatDataSourceOptions() With {.AddDisplayTextColumns = True})
				SetDataBindings(flatData)
				SetColorizer()
			End If
		End Sub
		Protected Overrides Function GetPrintableControl(ByVal customItemData As CustomItemData, ByVal exportInfo As CustomItemExportInfo) As XRControl
			Dim container As New PrintableComponentContainer()
			container.PrintableComponent = heatmap
			Return container
		End Function
		Private Sub ClearDataBindings()
			heatmap.DataAdapter = Nothing
			dataAdapter.YArgumentDataMember = Nothing
			dataAdapter.XArgumentDataMember = dataAdapter.YArgumentDataMember
			dataAdapter.ColorDataMember = dataAdapter.XArgumentDataMember
			dataAdapter.DataSource = dataAdapter.ColorDataMember
		End Sub
		Private Function ValidateBindings() As Boolean
			Return dashboardItem.Metadata.Value IsNot Nothing AndAlso dashboardItem.Metadata.Row IsNot Nothing AndAlso dashboardItem.Metadata.Column IsNot Nothing
		End Function
		Private Sub SetDataBindings(ByVal flatDataSource As DashboardFlatDataSource)
			dataAdapter.ColorDataMember = dashboardItem.Metadata.Value.UniqueId
			dataAdapter.XArgumentDataMember = flatDataSource.GetDisplayTextColumn(dashboardItem.Metadata.Column.UniqueId).Name
			dataAdapter.YArgumentDataMember = flatDataSource.GetDisplayTextColumn(dashboardItem.Metadata.Row.UniqueId).Name
			heatmap.Label.Pattern = "{" & flatDataSource.GetDisplayTextColumn(dashboardItem.Metadata.Value.UniqueId).Name & "}"
			heatmap.ToolTipTextPattern = String.Format("X Argument: {{{0}}}" & vbLf & "Y Argument: {{{1}}}" & vbLf & "Value: {{{2}}}", flatDataSource.GetDisplayTextColumn(dashboardItem.Metadata.Column.UniqueId).Name, flatDataSource.GetDisplayTextColumn(dashboardItem.Metadata.Row.UniqueId).Name, flatDataSource.GetDisplayTextColumn(dashboardItem.Metadata.Value.UniqueId).Name)

			Try
				dataAdapter.DataSource = flatDataSource
				heatmap.DataAdapter = dataAdapter
			Catch
				heatmap.DataAdapter = Nothing
				dataAdapter.DataSource = Nothing
			End Try
		End Sub
		Private Sub SetColorizer()
			Dim provider As New HeatmapRangeColorProvider()
			provider.ApproximateColors = True
			Dim rangeStops As New List(Of Double)()
			Dim usePercentRangeStops As Boolean = False
			If dashboardItem.Metadata.Scale Is Nothing Then
				usePercentRangeStops = True
				rangeStops = ValueMapScaleHelper.GetPercentRangeStops(10)
			Else
				Dim uniformScale As UniformHeatmapScale = TryCast(dashboardItem.Metadata.Scale, UniformHeatmapScale)
				If uniformScale IsNot Nothing Then
					usePercentRangeStops = True
					rangeStops = ValueMapScaleHelper.GetPercentRangeStops(uniformScale.LevelsCount)
				Else
					Dim customScale As CustomHeatmapScale = TryCast(dashboardItem.Metadata.Scale, CustomHeatmapScale)
					If customScale IsNot Nothing Then
						usePercentRangeStops = customScale.IsPercent
						rangeStops = customScale.RangeStops.ToList()
					End If
				End If
			End If
			Dim rangesCount As Integer = rangeStops.Count
			provider.LegendItemPattern = If(usePercentRangeStops, "{VP1:p0} - {VP2:p0}", "{V1} - {V2}")
			If usePercentRangeStops Then
				For Each rangeStop As Double In rangeStops
					provider.RangeStops.Add(New HeatmapRangeStop(rangeStop / 100, HeatmapRangeStopType.Percentage))
				Next rangeStop
				provider.RangeStops.Add(New HeatmapRangeStop(1, HeatmapRangeStopType.Percentage))
			Else
				For Each rangeStop As Double In rangeStops
					provider.RangeStops.Add(New HeatmapRangeStop(rangeStop, HeatmapRangeStopType.Absolute))
				Next rangeStop
			End If
			provider.Palette = GeneratePalette(rangesCount)
			heatmap.ColorProvider = provider
		End Sub
		Private Function GeneratePalette(ByVal rangesCount As Integer) As Palette
			Dim palette As New Palette("HeatmapCustomPalette")
			Dim colors As New List(Of Color)()
			If dashboardItem.Metadata.Palette Is Nothing Then
                Dim autoColors As ColorCollection = SkinMapPaletteHolder.GetGradientColors(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                Dim i As Integer = 0
				Do While i < rangesCount
					colors.Add(ValueMapScaleHelper.GetGradientColor(autoColors(0), autoColors(1), i, rangesCount))
					i += 1
				Loop
			Else
				Dim gradientPalette As GradientPalette = TryCast(dashboardItem.Metadata.Palette, GradientHeatmapPalette)
				If gradientPalette IsNot Nothing Then
					Dim i As Integer = 0
					Do While i < rangesCount
						colors.Add(ValueMapScaleHelper.GetGradientColor(gradientPalette.StartColor, gradientPalette.EndColor, i, rangesCount))
						i += 1
					Loop
				Else
					Dim customPalette As CustomPalette = TryCast(dashboardItem.Metadata.Palette, CustomHeatmapPalette)
					If customPalette IsNot Nothing Then
						colors = customPalette.Colors.ToList()
					End If
				End If
				If colors IsNot Nothing AndAlso colors.Count > 0 Then
					If colors.Count > rangesCount Then
						colors.RemoveRange(rangesCount, colors.Count - rangesCount)
					End If
					If colors.Count < rangesCount Then
						Dim countToAdd As Integer = rangesCount - colors.Count
						For i As Integer = 0 To countToAdd - 1
							colors.Add(colors(colors.Count - 1))
						Next i
					End If
				End If
			End If
			For Each color As Color In colors
				palette.Add(color)
			Next color
			Return palette
		End Function
	End Class

End Namespace
