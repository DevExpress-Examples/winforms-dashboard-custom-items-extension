using System;

namespace DevExpress.DashboardWin.CustomItemExtension.CustomItems.WebPageItem
{
    public partial class UriEditForm : DevExpress.XtraEditors.XtraForm {
		public string UriPattern { get { return teUriPattern.Text; } set { teUriPattern.Text = value; } }
        public UriEditForm() {
            InitializeComponent();
        }

		void OnInsertPlaceholderClick(object sender, EventArgs e) {
			const string placeholder = "{0}";
			int selectionStart = teUriPattern.SelectionStart;
			int selectionlength = teUriPattern.SelectionLength;
			string text = teUriPattern.Text;
			text = text.Remove(selectionStart, selectionlength).Insert(selectionStart, placeholder);
			teUriPattern.Text = text;
			teUriPattern.Focus();
			teUriPattern.Select(selectionStart, placeholder.Length);
		}
	}
}
