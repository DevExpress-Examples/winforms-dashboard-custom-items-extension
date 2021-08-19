using DevExpress.DashboardCommon;
using System.ComponentModel;

namespace DevExpresss.DashboardWin.CustomItemExtension {
    [
    DisplayName("Sunburst"),
    CustomItemDescription("Create a Sunburst dashboard item and insert it into the dashboard.\n\nThe Sunburst combines a TreeMap and a Pie chart to visualize hierarchical data in a circular layout."),
    CustomItemImage("DevExpresss.DashboardWin.CustomItemExtension.Images.SunburstCustomItem.svg")
    ]
    public class SunburstItemMetadata : CustomItemMetadata {
        readonly DimensionCollection arguments = new DimensionCollection();
        [
        DisplayName("Value"),
        EmptyDataItemPlaceholder("Value"),
        ]
        public Measure Value {
            get { return GetPropertyValue<Measure>(); }
            set { SetPropertyValue(value); }
        }
        [
        DisplayName("Arguments"),
        EmptyDataItemPlaceholder("Argument"),
        SupportColoring(DefaultColoringMode.None),
        SupportInteractivity
        ]
        public DimensionCollection Arguments { get { return arguments; } }
    }
}
