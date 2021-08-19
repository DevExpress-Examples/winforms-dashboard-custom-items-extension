using DevExpress.DashboardCommon;
using System.ComponentModel;

namespace DevExpresss.DashboardWin.CustomItemExtension
{
    [
    DisplayName("Tree List"),
    CustomItemDescription("Create a Tree List dashboard item and insert it into the dashboard.\n\nThis hybrid item combines a Tree List and Grid. The Tree List uses the parent-child relationships to generate the hierarchical data structure."),
    CustomItemImage("DevExpresss.DashboardWin.CustomItemExtension.Images.TreeListItem.svg")
    ]
    public class TreeListItemMetadata : CustomItemMetadata {
        [
        DisplayName("ID"),
        EmptyDataItemPlaceholder("ID"),
        SupportInteractivity
        ]
        public Dimension ID {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }
        [
        DisplayName("Parent ID"),
        EmptyDataItemPlaceholder("Parent ID"),
        SupportInteractivity
        ]
        public Dimension ParentID {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }
        [
        DisplayName("Dimensions"),
        EmptyDataItemPlaceholder("Dimension"),
        SupportInteractivity
        ]
        public DimensionCollection Dimensions { get; } = new DimensionCollection();
        [
        DisplayName("Measures"),
        EmptyDataItemPlaceholder("Measure")
        ]
        public MeasureCollection Measures { get; } = new MeasureCollection();


    }

}
