Imports System

Namespace DevExpresss.DashboardWin.CustomItemExtension.CustomItems.WebPageItem
	Partial Public Class UriEditForm
		Inherits DevExpress.XtraEditors.XtraForm

		Public Property UriPattern() As String
			Get
				Return teUriPattern.Text
			End Get
			Set(ByVal value As String)
				teUriPattern.Text = value
			End Set
		End Property
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub OnInsertPlaceholderClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnInsertPlaceholder.Click
			Const placeholder As String = "{0}"
			Dim selectionStart As Integer = teUriPattern.SelectionStart
			Dim selectionlength As Integer = teUriPattern.SelectionLength
'INSTANT VB NOTE: The variable text was renamed since Visual Basic does not handle local variables named the same as class members well:
			Dim text_Renamed As String = teUriPattern.Text
			text_Renamed = text_Renamed.Remove(selectionStart, selectionlength).Insert(selectionStart, placeholder)
			teUriPattern.Text = text_Renamed
			teUriPattern.Focus()
			teUriPattern.Select(selectionStart, placeholder.Length)
		End Sub
	End Class
End Namespace