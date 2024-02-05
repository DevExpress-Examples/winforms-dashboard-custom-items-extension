using System.ComponentModel;
using DevExpress.DashboardCommon;

namespace DevExpress.DashboardWin.CustomItemExtension {
    [
    DisplayName("Sankey"),
    CustomItemDescription("Create a Sankey diagram dashboard item and insert it into the dashboard.\n\nThe Sankey diagram visualizes data as weighted flows or relationships between nodes."),
    CustomItemImage("DevExpress.DashboardWin.CustomItemExtension.Images.SankeyCustomItem.svg")
    ]
    public class SankeyItemMetadata : CustomItemMetadata {
        [
        DisplayName("Weight"),
        EmptyDataItemPlaceholder("Weight")
        ]
        public Measure Weight {
            get { return GetPropertyValue<Measure>(); }
            set { SetPropertyValue(value); }
        }
        [
        DisplayName("Target"),
        EmptyDataItemPlaceholder("Target"),
        SupportColoring(DefaultColoringMode.None),
        SupportInteractivity
        ]
        public Dimension Target {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }
        [
        DisplayName("Source"),
        EmptyDataItemPlaceholder("Source"),
        SupportColoring(DefaultColoringMode.Hue),
        SupportInteractivity
        ]
        public Dimension Source {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }
    }
}
