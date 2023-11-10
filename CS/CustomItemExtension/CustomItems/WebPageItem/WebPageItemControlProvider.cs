using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DevExpresss.DashboardWin.CustomItemExtension
{
    public class WebPageItemControlProvider : CustomControlProviderBase {
        MemoryStream imageStream = new MemoryStream();
        bool imageIsReady = false;
        WebView2 webControl;
        CustomDashboardItem<WebPageItemMetadata> dashboardItem;
        protected override Control Control { get { return webControl; } }

        public WebPageItemControlProvider(CustomDashboardItem<WebPageItemMetadata> dashboardItem) {
            this.dashboardItem = dashboardItem;
            webControl = new WebView2();
            webControl.CoreWebView2InitializationCompleted += WebControl_CoreWebView2InitializationCompleted;
        }
        void PrepareImageStream()
        {
            imageStream = new MemoryStream();
            imageIsReady = false;
            webControl.CoreWebView2.CapturePreviewAsync(CoreWebView2CapturePreviewImageFormat.Png, imageStream).ContinueWith((t) => imageIsReady = true);
        }
        private void WebControl_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e) {
            webControl.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            webControl.CoreWebView2.NavigationCompleted += (s, ee) => PrepareImageStream();
            webControl.SizeChanged += (s, ee) => PrepareImageStream();
        }
        protected override void UpdateControl(CustomItemData customItemData) {
            try {
                string uriString = string.Empty;
                object attributeValue = null;
                if(dashboardItem.Metadata.Attribute != null) {
                    attributeValue = customItemData.GetFlatData().GetDisplayText(dashboardItem.Metadata.Attribute.UniqueId, 0);
                }
                if(dashboardItem.Metadata.URI != null) {
                    uriString = string.Format(dashboardItem.Metadata.URI, attributeValue);
                }
                else if(attributeValue != null) {
                    uriString = attributeValue.ToString();
                }
                webControl.Source = new Uri(uriString);
            }
            catch {
                webControl.Source = new Uri("about:blank");
            };
        }
        protected override XRControl GetPrintableControl(CustomItemData customItemData, CustomItemExportInfo exportInfo) {
            if(imageIsReady) {
                XRPictureBox xRPictureBox = new XRPictureBox();
                Bitmap bitmap = new Bitmap(imageStream);
                xRPictureBox.ImageSource = new ImageSource(bitmap);
                return xRPictureBox;
            }
            XRLabel label = new XRLabel();
            label.Text = "Image did not load.Try to execute the Export command again.";
            return label;
        }
    }
}
