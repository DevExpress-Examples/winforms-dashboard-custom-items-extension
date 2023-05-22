Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports DevExpress.XtraPrinting.Drawing
Imports DevExpress.XtraReports.UI
Imports Microsoft.Web.WebView2.Core
Imports Microsoft.Web.WebView2.WinForms
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Namespace DevExpresss.DashboardWin.CustomItemExtension
	Public Class WebPageItemControlProvider
		Inherits CustomControlProviderBase

		Private imageStream As New MemoryStream()
		Private imageIsReady As Boolean = False
		Private webControl As WebView2
		Private dashboardItem As CustomDashboardItem(Of WebPageItemMetadata)
		Protected Overrides ReadOnly Property Control() As Control
			Get
				Return webControl
			End Get
		End Property

		Public Sub New(ByVal dashboardItem As CustomDashboardItem(Of WebPageItemMetadata))
			Me.dashboardItem = dashboardItem
			webControl = New WebView2()
			AddHandler webControl.CoreWebView2InitializationCompleted, AddressOf WebControl_CoreWebView2InitializationCompleted
		End Sub
		Private Sub PrepareImageStream()
			imageStream = New MemoryStream()
			imageIsReady = False
			webControl.CoreWebView2.CapturePreviewAsync(CoreWebView2CapturePreviewImageFormat.Png, imageStream).ContinueWith(Sub(t) imageIsReady = True)
		End Sub
		Private Sub WebControl_CoreWebView2InitializationCompleted(ByVal sender As Object, ByVal e As Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs)
			webControl.CoreWebView2.Settings.AreDefaultContextMenusEnabled = False
			AddHandler webControl.CoreWebView2.NavigationCompleted, Sub(s, ee) PrepareImageStream()
			AddHandler webControl.SizeChanged, Sub(s, ee) PrepareImageStream()
		End Sub
		Protected Overrides Sub UpdateControl(ByVal customItemData As CustomItemData)
			Try
				Dim uriString As String = String.Empty
				Dim attributeValue As Object = Nothing
				If dashboardItem.Metadata.Attribute IsNot Nothing Then
					attributeValue = customItemData.GetFlatData().GetDisplayText(dashboardItem.Metadata.Attribute.UniqueId, 0)
				End If
				If dashboardItem.Metadata.URI IsNot Nothing Then
					uriString = String.Format(dashboardItem.Metadata.URI, attributeValue)
				ElseIf attributeValue IsNot Nothing Then
					uriString = attributeValue.ToString()
				End If
				webControl.Source = New Uri(uriString)
			Catch
				webControl.Source = New Uri("about:blank")
			End Try
		End Sub
		Protected Overrides Function GetPrintableControl(ByVal customItemData As CustomItemData, ByVal exportInfo As CustomItemExportInfo) As XRControl
			If imageIsReady Then
				Dim xRPictureBox As New XRPictureBox()
				Dim bitmap As New Bitmap(imageStream)
				xRPictureBox.ImageSource = New ImageSource(bitmap)
				Return xRPictureBox
			End If
			Dim label As New XRLabel()
			label.Text = "Image did not load.Try to execute the Export command again."
			Return label
		End Function
	End Class
End Namespace
