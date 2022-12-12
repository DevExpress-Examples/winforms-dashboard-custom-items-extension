using System.ComponentModel;
using DevExpress.DashboardCommon;

namespace DevExpresss.DashboardWin.CustomItemExtension {
    [
    DisplayName("Waypoint Map"),
    CustomItemDescription("Create a Waypoint map dashboard item and insert it into the dashboard.\n\nThe Waypoint map visualizes data as linked points."),
    CustomItemImage("DevExpresss.DashboardWin.CustomItemExtension.Images.WaypointCustomItem.svg")
    ]
    public class WaypointMapItemMetadata : CustomItemMetadata {
        [
        DisplayName("Source Latitude"),
        EmptyDataItemPlaceholder("Latitude"),
        SupportInteractivity,
        SupportedDataTypes(DataSourceFieldType.Numeric)
        ]
        public Dimension SourceLatitude {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }
        [
        DisplayName("Source Longitude"),
        EmptyDataItemPlaceholder("Longitude"),
        SupportInteractivity,
        SupportedDataTypes(DataSourceFieldType.Numeric)
        ]
        public Dimension SourceLongitude {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }
        [
        DisplayName("Target Latitude"),
        EmptyDataItemPlaceholder("Latitude"),
        SupportInteractivity,
        SupportedDataTypes(DataSourceFieldType.Numeric)
        ]
        public Dimension TargetLatitude {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }
        [
        DisplayName("Target Longitude"),
        EmptyDataItemPlaceholder("Longitude"),
        SupportInteractivity,
        SupportedDataTypes(DataSourceFieldType.Numeric)
        ]
        public Dimension TargetLongitude {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }
    }
}
