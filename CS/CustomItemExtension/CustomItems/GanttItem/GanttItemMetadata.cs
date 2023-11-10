using DevExpress.DashboardCommon;
using System.ComponentModel;

namespace DevExpresss.DashboardWin.CustomItemExtension
{
    [DisplayName("Gantt"),
CustomItemDescription("Create a Gantt dashboard item and insert it into the dashboard.\n\nThe Gantt item dispalys tasks organized in a tree list with a bar chart that visualizes a schedule data."),
CustomItemImage("DevExpresss.DashboardWin.CustomItemExtension.Images.GanttItem.svg")]
    public class GanttItemMetadata : CustomItemMetadata {
        [DisplayName("ID"),
        EmptyDataItemPlaceholder("ID"),
        SupportInteractivity]
        public Dimension ID {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }

        [DisplayName("Parent ID"),
        EmptyDataItemPlaceholder("Parent ID"),
        SupportInteractivity]
        public Dimension ParentID {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }

        [DisplayName("Text"),
        EmptyDataItemPlaceholder("Text"),
        SupportColoring(DefaultColoringMode.None),
        SupportInteractivity]
        public Dimension Text {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }

        [DisplayName("Start Date"),
        EmptyDataItemPlaceholder("Start Date"),
        SupportedDataTypes(DataSourceFieldType.DateTime),
        DefaultGroupInterval(DateTimeGroupInterval.DayMonthYear),
        SupportInteractivity]
        public Dimension StartDate {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }

        [DisplayName("Finish Date"),
        EmptyDataItemPlaceholder("Finish Date"),
        SupportedDataTypes(DataSourceFieldType.DateTime),
        DefaultGroupInterval(DateTimeGroupInterval.DayMonthYear),
        SupportInteractivity]
        public Dimension FinishDate {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }
    }
}
