Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin.Native
Imports DevExpress.XtraLayout
Imports DevExpress.XtraLayout.Utils
Imports DevExpress.DashboardWin.CustomItemExtension

Namespace DevExpress.DashboardWin.CustomItemExtension.Heatmap
	Partial Public Class HeatmapOptionsForm
		Inherits DashboardForm

		Public Sub New()
			InitializeComponent()
		End Sub
		Public Sub New(ByVal palette As MapPalette, ByVal scale As MapScale)
			Me.New()
			valueMapControl.InitializeFrom(palette, scale)
		End Sub
		Public Function GetScale() As MapScale
			Return valueMapControl.GetScale()
		End Function

		Public Function GetPalette() As MapPalette
			Return valueMapControl.GetPalette()
		End Function
		Private Sub OnBtnOKClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
			DialogResult = DialogResult.OK
		End Sub
		Private Sub OnBtnCancelClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
			DialogResult = DialogResult.Cancel
		End Sub
	End Class
End Namespace
