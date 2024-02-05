Imports System

Namespace DevExpress.DashboardWin.CustomItemExtension.CustomItems.WebPageItem
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
			Dim _text As String = teUriPattern.Text
			_text = _text.Remove(selectionStart, selectionlength).Insert(selectionStart, placeholder)
			teUriPattern.Text = _text
			teUriPattern.Focus()
			teUriPattern.Select(selectionStart, placeholder.Length)
		End Sub
	End Class
End Namespace
