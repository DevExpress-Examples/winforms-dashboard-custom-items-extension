using DevExpress.DashboardCommon;
using System;
using System.ComponentModel;

namespace DevExpress.DashboardWin.CustomItemExtension
{
    [DisplayName("Web Page"),
    CustomItemDescription("Create a Web Page dashboard item and insert it into the dashboard.\n\nThis item displays the web content. You can specify the URI pattern to create the page address from a data column at run-time."),
    CustomItemImage("DevExpress.DashboardWin.CustomItemExtension.Images.WebPageItem.svg")]
    public class WebPageItemMetadata : CustomItemMetadata {
        [
        DisplayName("Attribute"),
        EmptyDataItemPlaceholder("Attribute"),
        ]
        public Dimension Attribute {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }
        [UrlCustom]
        public string URI {
            get { return GetCustomPropertyValue<string>(); }
            set { SetCustomPropertyValue(value); }
        }

        public class UrlCustomAttribute : Attribute, ICustomPropertyValueConverterAttribute<string>, ICustomPropertyHistoryAttribute<string> {

            public string ConvertToString(string value) {
                return value;
            }
            public string ConvertFromString(string strValue) {
                return strValue;
            }
            public string GetHistoryMessage(string newValue, string oldValue, string dashboardItemName) {
                return "URI for the " + dashboardItemName + " changed";
            }
        }
    }
}
