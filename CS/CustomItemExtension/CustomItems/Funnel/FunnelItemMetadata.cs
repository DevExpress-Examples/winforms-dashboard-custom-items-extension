using System;
using System.ComponentModel;
using DevExpress.DashboardCommon;
using DevExpress.XtraCharts;

namespace DevExpress.DashboardWin.CustomItemExtension {
    [DisplayName("Funnel"),
    CustomItemDescription("Create a Funnel chart dashboard item and insert it into the dashboard.\n\nThe Funnel chart visualizes the progressive reduction of data as it passes from one stage to another in a process or procedure."),
    CustomItemImage("DevExpress.DashboardWin.CustomItemExtension.Images.FunnelItem.svg")]
    public class FunnelItemMetadata : CustomItemMetadata {
        [DisplayName("Value"),
        EmptyDataItemPlaceholder("Value"),
        SupportColoring(DefaultColoringMode.None)]
        public Measure Value {
            get { return GetPropertyValue<Measure>(); }
            set { SetPropertyValue(value); }
        }
        [DisplayName("Arguments"),
        EmptyDataItemPlaceholder("Argument"),
        SupportColoring(DefaultColoringMode.Hue),
        SupportInteractivity]
        public DimensionCollection Arguments { get; } = new DimensionCollection();

    }
}
