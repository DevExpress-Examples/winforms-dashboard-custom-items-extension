using DevExpress.DashboardCommon;
using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace DevExpresss.DashboardWin.CustomItemExtension {
    [
    DisplayName("Heatmap"),
    CustomItemDescription("Create a Heatmap dashboard item and insert it into the dashboard.\n\n The Heatmap item uses color variation to show the relationship between two dimensions."),
    CustomItemImage("DevExpresss.DashboardWin.CustomItemExtension.Images.HeatmapCustomItem.svg")
    ]
    public class HeatmapItemMetadata : CustomItemMetadata {
        readonly DimensionCollection rows = new DimensionCollection();
        readonly DimensionCollection columns = new DimensionCollection();
        [
        DisplayName("Value"),
        EmptyDataItemPlaceholder("Value"),
        ]
        public Measure Value {
            get { return GetPropertyValue<Measure>(); }
            set { SetPropertyValue(value); }
        }
        [
        DisplayName("Row"),
        EmptyDataItemPlaceholder("Row"),
        SupportInteractivity
        ]
        public Dimension Row {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }
        [
        DisplayName("Column"),
        EmptyDataItemPlaceholder("Column"),
        SupportInteractivity
        ]
        public Dimension Column {
            get { return GetPropertyValue<Dimension>(); }
            set { SetPropertyValue(value); }
        }
        [MapPaletteCustom]
        public MapPalette Palette {
            get { return GetCustomPropertyValue<MapPalette>(); }
            set { SetCustomPropertyValue(value); }
        }
        [MapScaleCustom]
        public MapScale Scale {
            get { return GetCustomPropertyValue<MapScale>(); }
            set { SetCustomPropertyValue(value); }
        }
        [ShowLabelsCustom]
        public bool ShowLabels {
            get { return GetCustomPropertyValue<bool>(); }
            set { SetCustomPropertyValue(value); }
        }
        [ShowLegendCustom]
        public bool ShowLegend {
            get { return GetCustomPropertyValue<bool>(); }
            set { SetCustomPropertyValue(value); }
        }
        [EnableZoomingXCustom]
        public bool EnableZoomingXAxis {
            get { return GetCustomPropertyValue<bool>(); }
            set { SetCustomPropertyValue(value); }
        }
        [EnableZoomingYCustom]
        public bool EnableZoomingYAxis {
            get { return GetCustomPropertyValue<bool>(); }
            set { SetCustomPropertyValue(value); }
        }
    }
    public class MapPaletteCustomAttribute : Attribute, ICustomPropertyValueConverterAttribute<MapPalette>, ICustomPropertyHistoryAttribute<MapPalette> {
        public string ConvertToString(MapPalette mapPalette) {
            if(mapPalette is CustomHeatmapPalette)
                return (mapPalette as CustomHeatmapPalette).ToJson();
            if(mapPalette is GradientHeatmapPalette)
                return (mapPalette as GradientHeatmapPalette).ToJson();
            return string.Empty;
        }
        public MapPalette ConvertFromString(string strValue) {
            CustomHeatmapPalette customPalette = CustomHeatmapPalette.FromJson(strValue);
            if(customPalette != null)
                return customPalette;
            GradientHeatmapPalette gradientPalette = GradientHeatmapPalette.FromJson(strValue);
            if(gradientPalette != null)
                return gradientPalette;
            return null;
        }
        public string GetHistoryMessage(MapPalette newValue, MapPalette oldValue, string dashboardItemName) {
            return "Palette for the " + dashboardItemName + " changed";
        }
    }
    public class MapScaleCustomAttribute : Attribute, ICustomPropertyValueConverterAttribute<MapScale>, ICustomPropertyHistoryAttribute<MapScale> {
        public string ConvertToString(MapScale mapScale) {
            if(mapScale is CustomHeatmapScale)
                return (mapScale as CustomHeatmapScale).ToJson();
            if(mapScale is UniformHeatmapScale)
                return (mapScale as UniformHeatmapScale).ToJson();
            return string.Empty;
        }
        public MapScale ConvertFromString(string strValue) {
                CustomHeatmapScale customScale = CustomHeatmapScale.FromJson(strValue);
                if(customScale != null)
                    return customScale;
                UniformHeatmapScale uniformScale = UniformHeatmapScale.FromJson(strValue);
                if(uniformScale != null)
                    return uniformScale;
            return null;
        }
        public string GetHistoryMessage(MapScale newValue, MapScale oldValue, string dashboardItemName) {
            return "Scale for the " + dashboardItemName + " changed";
        }
    }
    public class CustomHeatmapScale : CustomScale {
        public string ToJson() {
            return JsonConvert.SerializeObject(this);
        }
        public static CustomHeatmapScale FromJson(string json) {
            try {
                return JsonConvert.DeserializeObject<CustomHeatmapScale>(
                    json,
                    new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Error });
            }
            catch {
                return null;
            }
        }
    }
    public class UniformHeatmapScale : UniformScale {
        public string ToJson() {
            return JsonConvert.SerializeObject(this);
        }
        public static UniformHeatmapScale FromJson(string json) {
            try {
                return JsonConvert.DeserializeObject<UniformHeatmapScale>(
                json,
                new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Error });
            }
            catch {
                return null;
            }
        }
    }
    public class CustomHeatmapPalette : CustomPalette {
        public string ToJson() {
            return JsonConvert.SerializeObject(this);
        }
        public static CustomHeatmapPalette FromJson(string json) {
            try {
                return JsonConvert.DeserializeObject<CustomHeatmapPalette>(
                    json,
                    new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Error });
            }
            catch {
                return null;
            }
        }
    }
    public class GradientHeatmapPalette : GradientPalette {
        public string ToJson() {
            return JsonConvert.SerializeObject(this);
        }
        public static GradientHeatmapPalette FromJson(string json) {
            try {
                return JsonConvert.DeserializeObject<GradientHeatmapPalette>(
                    json,
                    new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Error });
            }
            catch { return null; }
        }
    }
    public class ShowLabelsCustomAttribute : Attribute, ICustomPropertyValueConverterAttribute<bool>, ICustomPropertyHistoryAttribute<bool> {
        public string ConvertToString(bool value) {
            return value.ToString();
        }
        public bool ConvertFromString(string strValue) {
            bool result;
            Boolean.TryParse(strValue, out result);
            return result;
        }
        public string GetHistoryMessage(bool newValue, bool oldValue, string dashboardItemName) {
            return (newValue ? "Enable" : "Disable") + " Show labels for the " + dashboardItemName;
        }
    }
    public class ShowLegendCustomAttribute : Attribute, ICustomPropertyValueConverterAttribute<bool>, ICustomPropertyHistoryAttribute<bool> {
        public string ConvertToString(bool value) {
            return value.ToString();
        }
        public bool ConvertFromString(string strValue) {
            bool result;
            Boolean.TryParse(strValue, out result);
            return result;
        }
        public string GetHistoryMessage(bool newValue, bool oldValue, string dashboardItemName) {
            return (newValue ? "Enable" : "Disable") + " legend visibility for " + dashboardItemName;
        }
    }
    public class EnableZoomingXCustomAttribute : Attribute, ICustomPropertyValueConverterAttribute<bool>, ICustomPropertyHistoryAttribute<bool> {
        public string ConvertToString(bool value) {
            return value.ToString();
        }
        public bool ConvertFromString(string strValue) {
            bool result;
            Boolean.TryParse(strValue, out result);
            return result;
        }
        public string GetHistoryMessage(bool newValue, bool oldValue, string dashboardItemName) {
            return (newValue ? "Enable" : "Disable") + " X-axis scaling for " + dashboardItemName;
        }
    }
    public class EnableZoomingYCustomAttribute : Attribute, ICustomPropertyValueConverterAttribute<bool>, ICustomPropertyHistoryAttribute<bool> {
        public string ConvertToString(bool value) {
            return value.ToString();
        }
        public bool ConvertFromString(string strValue) {
            bool result;
            Boolean.TryParse(strValue, out result);
            return result;
        }
        public string GetHistoryMessage(bool newValue, bool oldValue, string dashboardItemName) {
            return (newValue ? "Enable" : "Disable") + " Y-axis scaling for " + dashboardItemName;
        }
    }
}
