# DevExpress WinForms BI Dashboard - Custom Items

This package provides additional data presentation and analysis items for the [DevExpress WinForms Dashboard](https://docs.devexpress.com/Dashboard/402519/winforms-dashboard). Install the package and register the required items as shown below.

## Included Items

- Sankey Item - Visualizes weighted flows and relationships between nodes.
- Sunburst Item - Displays hierarchical data in a circular layout.
- Tree List Item - Displays hierarchical parent-child data in a tree grid.
- Waypoint Map Item - Displays linked geographic locations on a map.
- Funnel Item - Visualizes data as stages in a business or sales process.
- Gantt Item - Displays project schedules, tasks, and timelines.
- Web Page Item - Displays web pages and dynamically generated URLs.
- Heatmap Item - Visualizes relationships between two dimensions using color intensity.

## Evaluation Period, Pricing Options, and Licensing Terms

Whether you own a license or want to start a 30-day evaluation, register your DevExpress license key. To obtain your key, sign in to the [DevExpress Download Manager](https://www.devexpress.com/ClientCenter/DownloadManager/). Use your existing DevExpress.com with an existing DevExpress.com account or create a free account. For additional registration instructions, see [License Key for DevExpress Products]( https://www.devexpress.com/go/Licensing_Documentation.aspx).

If you purchase a license after evaluating a product, re-register your updated DevExpress license key to remove evaluation warnings and messages.

This package is included in the following commercial subscription: [DevExpress Universal Subscription](https://www.devexpress.com/subscriptions/universal.xml).

See [DevExpress Subscriptions & Licensing](https://www.devexpress.com/buy) for subscription comparison tables, purchasing FAQ, and licensing information. 

## Integrate Required Dashboard Items to Your Project

### Prerequisites  

Web Page item requires [WebView2 Runtime](https://developer.microsoft.com/en-us/microsoft-edge/webview2/) to be installed on your machine.

### Install the Package


```
dotnet add package DevExpress.Win.Dashboard.CustomItemExtension
```

### Register Required Items

Code examples in this section register the Sankey Item.

1. Register [CustomItemMetadata](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.CustomItemMetadata) types associated with required items:


    **C# code**:
    ```
    using DevExpress.DashboardWin.CustomItemExtension;
    using DevExpress.DashboardCommon;

    namespace CustomItemsSample {
        static class Program {
            /// <summary>
            /// The main entry point for the application.
            /// </summary>
            [STAThread]
            static void Main() {
                //...
                Dashboard.CustomItemMetadataTypes.Register<SankeyItemMetadata>();
                Application.Run(new Form1());
            }
        }
    }
    ```

    **VB code**: 
    ```
    Imports DevExpress.DashboardWin.CustomItemExtension
    Imports DevExpress.DashboardCommon

    Namespace CustomItemsSample
        Friend NotInheritable Class Program
            ''' <summary>
            ''' The main entry point for the application.
            ''' </summary>
            Private Sub New()
            End Sub
            <STAThread> _
            Shared Sub Main()
            '...
            Dashboard.CustomItemMetadataTypes.Register(Of SankeyItemMetadata)()
            Application.Run(New Form1())
            End Sub
        End Class
    End Namespace
    ```
2. Call the following code to attach the extension to the `DashboardDesigner` control:

    **C# code**:
    ```
    using DevExpress.XtraBars.Ribbon;
    using DevExpress.DashboardWin.CustomItemExtension; 

    public Form1() {
            InitializeComponent();
        var sankeyItemModule = new SankeyItemExtensionModule();
        dashboardDesigner1.CreateRibbon();
            sankeyItemModule.AttachDesigner(dashboardDesigner1);
    }
    ``` 

    **VB code**: 
    ```
    Imports DevExpress.XtraBars.Ribbon
    Imports DevExpress.DashboardWin.CustomItemExtension

    Public Sub New()
        InitializeComponent()
        Dim sankeyItemModule = New SankeyItemExtensionModule()
        dashboardDesigner1.CreateRibbon()
        sankeyItemModule.AttachDesigner(dashboardDesigner1)
    End Sub 
    ```
## Documentation

- [Custom Item Tutorials](http://docs.devexpress.com/Dashboard/403031/winforms-dashboard/winforms-designer/create-a-custom-item#tutorials)
- [Custom Item Troubleshooting](https://docs.devexpress.com/Dashboard/403250/winforms-dashboard/winforms-designer/create-a-custom-item/custom-item-troubleshooting)

## Software Bill of Materials (SBOM)

For information about SBOM files for DevExpress packages, see [Software Bill of Materials (SBOM) for DevExpress .NET assemblies/NuGet packages, JavaScript, VCL and other redistributable artifacts](https://www.devexpress.com/go/DevExpress_Security_SBOM.aspx).

**See Also**: [Information Security](https://www.devexpress.com/support/information-security.xml) | [Security - What You Need to Know](https://docs.devexpress.com/GeneralInformation/403365)

## Useful Online Resources

- [www.devexpress.com](https://www.devexpress.com/)
- [DevExpress Support Center](https://www.devexpress.com/Support/Center/)
- [DevExpress Blogs](https://community.devexpress.com/blogs/)
