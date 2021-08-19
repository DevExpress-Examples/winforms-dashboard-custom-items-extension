using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using DevExpress.XtraMap;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DevExpresss.DashboardWin.CustomItemExtension {
    public class WaypointMapItemControlProvider : CustomControlProviderBase {
        public static string BingCopyright { get { return "Copyright © " + DateTime.Now.Year + " Microsoft and its suppliers. All rights reserved."; } }

        readonly Color defaultLineColor = Color.FromArgb(125, 255, 212, 106);
        readonly Color highlightLineColor = Color.FromArgb(200, 255, 212, 106);
        readonly Color SelectionLineColor = Color.FromArgb(255, 255, 212, 106);

        CustomDashboardItem<WaypointMapItemMetadata> dashboardItem;
        MapControl map;
        VectorItemsLayer vectorLayer;
        MapItemStorage mapItemStorage;
        MapOverlayTextItem validationInfoItem;
        DashboardFlatDataSource flatData;
        bool skipSelectionEvent = false;

        protected override Control Control { get { return map; } }
        public WaypointMapItemControlProvider(CustomDashboardItem<WaypointMapItemMetadata> dashboardItem, string bingKey) {
            this.dashboardItem = dashboardItem;
            map = new MapControl();
            map.MapItemClick += MapItemClickHandler;
            map.SelectionChanged += Map_SelectionChanged;
            map.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            map.EnableRotation = false;
            map.NavigationPanelOptions.Visible = false;
            ImageLayer imageLayer = new ImageLayer() { Transparency = 1 };
            mapItemStorage = new MapItemStorage();
            vectorLayer = new VectorItemsLayer();
            vectorLayer.Data = mapItemStorage;
            map.Layers.Add(imageLayer);
            map.Layers.Add(vectorLayer);
            imageLayer.DataProvider = new BingMapDataProvider() { BingKey = bingKey };
            SetupMapOverlays();
        }
        protected override void UpdateControl(CustomItemData customItemData) {
            flatData = customItemData.GetFlatData();
            mapItemStorage.Items.BeginUpdate();
            mapItemStorage.Items.Clear();
            if(ValidateBindings())
                PopulateMapItems(flatData);
            mapItemStorage.Items.EndUpdate();
            map.ZoomToFitLayerItems();
            SetSelectionMode();
        }
        protected override void SetSelection(CustomItemSelection selection) {
            skipSelectionEvent = true;
            vectorLayer.SelectedItems.Clear();
            IList<DashboardFlatDataSourceRow> selectedRows = selection.GetDashboardFlatDataSourceRows(flatData);
            var selectedLines = mapItemStorage.Items.Where(item => selectedRows.Contains(item.Tag));
            vectorLayer.SelectedItems.AddRange(selectedLines.ToList());
            skipSelectionEvent = false;
        }
        protected override XRControl GetPrintableControl(CustomItemData customItemData, CustomItemExportInfo exportInfo) {
            PrintableComponentContainer container = new PrintableComponentContainer();
            container.PrintableComponent = map;
            return container;
        }
        void Map_SelectionChanged(object sender, MapSelectionChangedEventArgs e) {
            if(skipSelectionEvent) return;
            var selectedRows = e.Selection.OfType<MapPolyline>().Select(polyline => polyline.Tag).OfType<DashboardFlatDataSourceRow>();

            if(selectedRows.Count() > 0 && Interactivity.CanSetMasterFilter)
                Interactivity.SetMasterFilter(selectedRows);
            else if(Interactivity.CanClearMasterFilter)
                Interactivity.ClearMasterFilter();
        }
        void MapItemClickHandler(object sender, MapItemClickEventArgs e) {
            e.Handled = e.MouseArgs.Button == MouseButtons.Right;
        }
        bool ValidateBindings() {
            if(Interactivity.IsDrillDownEnabled) {
                validationInfoItem.Text = "Waypoint Map Item does not support Drill-Down";
                validationInfoItem.Visible = true;
                return false;
            } else
                validationInfoItem.Visible = false;
            return dashboardItem.Metadata.SourceLatitude != null && dashboardItem.Metadata.SourceLongitude != null &&
                dashboardItem.Metadata.TargetLatitude != null && dashboardItem.Metadata.TargetLongitude != null;
        }
        void PopulateMapItems(DashboardFlatDataSource flatData) {
            foreach(DashboardFlatDataSourceRow dataRow in flatData) {
                CartesianPoint startPoint = new CartesianPoint(
                    Convert.ToDouble(flatData.GetValue(dashboardItem.Metadata.SourceLongitude.UniqueId, dataRow)),
                    Convert.ToDouble(flatData.GetValue(dashboardItem.Metadata.SourceLatitude.UniqueId, dataRow)));
                CartesianPoint endPoint = new CartesianPoint(
                    Convert.ToDouble(flatData.GetValue(dashboardItem.Metadata.TargetLongitude.UniqueId, dataRow)),
                    Convert.ToDouble(flatData.GetValue(dashboardItem.Metadata.TargetLatitude.UniqueId, dataRow)));
                var polyline = new MapPolyline() { Tag = dataRow };
                polyline.Points.AddRange(new CartesianPoint[] {
                    startPoint,
                    endPoint
                });
                SetPolylineDrawOptions(polyline);
                mapItemStorage.Items.Add(polyline);
            }
        }
        void SetSelectionMode() {
            switch(Interactivity.MasterFilterMode) {
                case DashboardItemMasterFilterMode.None:
                    map.SelectionMode = ElementSelectionMode.None;
                    vectorLayer.EnableHighlighting = false;
                    return;
                case DashboardItemMasterFilterMode.Multiple:
                    map.SelectionMode = ElementSelectionMode.Extended;
                    vectorLayer.EnableHighlighting = true;
                    break;
                case DashboardItemMasterFilterMode.Single:
                    map.SelectionMode = ElementSelectionMode.Single;
                    vectorLayer.EnableHighlighting = true;
                    break;
            }
        }
        void SetPolylineDrawOptions(MapPolyline shape) {
            shape.IsGeodesic = true;
            shape.Stroke = defaultLineColor;
            shape.StrokeWidth = 3;
            shape.SelectedStroke = SelectionLineColor;
            shape.SelectedStrokeWidth = 4;
            shape.HighlightedStroke = highlightLineColor;
            shape.HighlightedStrokeWidth = 4;
        }
        void SetupMapOverlays() {
            MapOverlay validationOverlay = new MapOverlay() { Alignment = ContentAlignment.MiddleCenter };
            validationInfoItem = new MapOverlayTextItem() { Visible = false };
            validationOverlay.Items.Add(validationInfoItem);
            map.Overlays.Add(validationOverlay);

            MapOverlay copyrightOverlay = new MapOverlay() { Alignment = ContentAlignment.BottomRight };
            using(Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DevExpresss.DashboardWin.CustomItemExtension.Images.BingLogo.png")) {
                if(stream != null) {
                    MapOverlayImageItem copyrightImageItem = new MapOverlayImageItem();
                    copyrightImageItem.Image = Image.FromStream(stream);
                    copyrightImageItem.Alignment = ContentAlignment.MiddleLeft;
                    copyrightOverlay.Items.Add(copyrightImageItem);
                }
            }
            MapOverlayTextItem copyrightLabelItem = new MapOverlayTextItem();
            copyrightLabelItem.Text = BingCopyright;
            copyrightLabelItem.Alignment = ContentAlignment.MiddleRight;
            copyrightOverlay.Items.Add(copyrightLabelItem);
            map.Overlays.Add(copyrightOverlay);
        }
    }
}
