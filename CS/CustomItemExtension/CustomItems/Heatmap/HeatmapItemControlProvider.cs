using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.DashboardCommon.Native;
using DevExpress.DashboardCommon.ViewerData;
using DevExpress.DashboardCommon.ViewModel;
using DevExpress.DashboardWin;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Heatmap;
using DevExpress.XtraMap;
using DevExpress.XtraMap.Native;
using DevExpress.XtraReports.UI;
using HeatmapDataSourceAdapter = DevExpress.XtraCharts.Heatmap.HeatmapDataSourceAdapter;

namespace DevExpresss.DashboardWin.CustomItemExtension {
    public class HeatmapItemControlProvider : CustomControlProviderBase {
        HeatmapControl heatmap;
        HeatmapDataSourceAdapter dataAdapter;
        CustomDashboardItem<HeatmapItemMetadata> dashboardItem;
        DashboardFlatDataSource flatData;
        protected override Control Control { get { return heatmap; } }
        public HeatmapItemControlProvider(CustomDashboardItem<HeatmapItemMetadata> dashboardItem) {
            this.dashboardItem = dashboardItem;
            heatmap = new HeatmapControl();
            this.dataAdapter = new HeatmapDataSourceAdapter();
            heatmap.ToolTipEnabled = true;
        }
        protected override void UpdateControl(CustomItemData customItemData) {
            heatmap.Label.Visible = dashboardItem.Metadata.ShowLabels;
            heatmap.Legend.Visibility = dashboardItem.Metadata.ShowLegend ? DefaultBoolean.True : DefaultBoolean.False;
            heatmap.EnableAxisXZooming = heatmap.EnableAxisXScrolling = dashboardItem.Metadata.EnableZoomingXAxis;
            heatmap.EnableAxisYZooming = heatmap.EnableAxisYScrolling = dashboardItem.Metadata.EnableZoomingYAxis;
            ClearDataBindings();
            if(ValidateBindings()) {
                flatData = customItemData.GetFlatData(new DashboardFlatDataSourceOptions() { AddDisplayTextColumns = true });
                SetDataBindings(flatData);
                SetColorizer();
            }
        }
        protected override XRControl GetPrintableControl(CustomItemData customItemData, CustomItemExportInfo exportInfo) {
            PrintableComponentContainer container = new PrintableComponentContainer();
            container.PrintableComponent = heatmap;
            return container;
        }
        void ClearDataBindings() {
            heatmap.DataAdapter = null;
            dataAdapter.DataSource = dataAdapter.ColorDataMember = dataAdapter.XArgumentDataMember = dataAdapter.YArgumentDataMember = null;
        }
        bool ValidateBindings() {
            return dashboardItem.Metadata.Value != null
                && dashboardItem.Metadata.Row != null
                && dashboardItem.Metadata.Column != null;
        }
        void SetDataBindings(DashboardFlatDataSource flatDataSource) {
            dataAdapter.ColorDataMember = dashboardItem.Metadata.Value.UniqueId;
            dataAdapter.XArgumentDataMember = flatDataSource.GetDisplayTextColumn(dashboardItem.Metadata.Column.UniqueId).Name;
            dataAdapter.YArgumentDataMember = flatDataSource.GetDisplayTextColumn(dashboardItem.Metadata.Row.UniqueId).Name;
            heatmap.Label.Pattern = "{" + flatDataSource.GetDisplayTextColumn(dashboardItem.Metadata.Value.UniqueId).Name + "}";
            heatmap.ToolTipTextPattern = string.Format(
                "X Argument: {{{0}}}\nY Argument: {{{1}}}\nValue: {{{2}}}",
                flatDataSource.GetDisplayTextColumn(dashboardItem.Metadata.Column.UniqueId).Name,
                flatDataSource.GetDisplayTextColumn(dashboardItem.Metadata.Row.UniqueId).Name,
                flatDataSource.GetDisplayTextColumn(dashboardItem.Metadata.Value.UniqueId).Name
                );

            try {
                dataAdapter.DataSource = flatDataSource;
                heatmap.DataAdapter = dataAdapter;
            }
            catch {
                heatmap.DataAdapter = null;
                dataAdapter.DataSource = null;
            }
        }
        void SetColorizer() {
            HeatmapRangeColorProvider provider = new HeatmapRangeColorProvider();
            provider.ApproximateColors = true;
            List<double> rangeStops = new List<double>();
            bool usePercentRangeStops = false;
            if(dashboardItem.Metadata.Scale == null) {
                usePercentRangeStops = true;
                rangeStops = ValueMapScaleHelper.GetPercentRangeStops(10);
            }
            else {
                UniformHeatmapScale uniformScale = dashboardItem.Metadata.Scale as UniformHeatmapScale;
                if(uniformScale != null) {
                    usePercentRangeStops = true;
                    rangeStops = ValueMapScaleHelper.GetPercentRangeStops(uniformScale.LevelsCount);
                }
                else {
                    CustomHeatmapScale customScale = dashboardItem.Metadata.Scale as CustomHeatmapScale;
                    if(customScale != null) {
                        usePercentRangeStops = customScale.IsPercent;
                        rangeStops = customScale.RangeStops.ToList();
                    }
                }
            }
            int rangesCount = rangeStops.Count;
            provider.LegendItemPattern = usePercentRangeStops ? "{VP1:p0} - {VP2:p0}" : "{V1} - {V2}";
            if(usePercentRangeStops) {
                foreach(double rangeStop in rangeStops) {
                    provider.RangeStops.Add(new HeatmapRangeStop(rangeStop / 100, HeatmapRangeStopType.Percentage));
                }
                provider.RangeStops.Add(new HeatmapRangeStop(1, HeatmapRangeStopType.Percentage));
            }
            else
                foreach(double rangeStop in rangeStops) {
                    provider.RangeStops.Add(new HeatmapRangeStop(rangeStop, HeatmapRangeStopType.Absolute));
            }
            provider.Palette = GeneratePalette(rangesCount);
            heatmap.ColorProvider = provider;
        }
        Palette GeneratePalette(int rangesCount) {
            Palette palette = new Palette("HeatmapCustomPalette");
            List<Color> colors = new List<Color>();
            if(dashboardItem.Metadata.Palette == null) {
                ColorCollection autoColors = ColorizerPaletteHelper.GetGradientColors(DevExpress.LookAndFeel.UserLookAndFeel.Default);
                for(int i = 0; i < rangesCount; i++)
                    colors.Add(ValueMapScaleHelper.GetGradientColor(autoColors[0], autoColors[1], i, rangesCount));
            }
            else {
                GradientPalette gradientPalette = dashboardItem.Metadata.Palette as GradientHeatmapPalette;
                if(gradientPalette != null) {
                    for(int i = 0; i < rangesCount; i++)
                        colors.Add(ValueMapScaleHelper.GetGradientColor(gradientPalette.StartColor, gradientPalette.EndColor, i, rangesCount));
                }
                else {
                    CustomPalette customPalette = dashboardItem.Metadata.Palette as CustomHeatmapPalette;
                    if(customPalette != null)
                        colors = customPalette.Colors.ToList();
                }
                if(colors != null && colors.Count > 0) {
                    if(colors.Count > rangesCount)
                        colors.RemoveRange(rangesCount, colors.Count - rangesCount);
                    if(colors.Count < rangesCount) {
                        int countToAdd = rangesCount - colors.Count;
                        for(int i = 0; i < countToAdd; i++)
                            colors.Add(colors[colors.Count - 1]);
                    }
                }
            }
            foreach(Color color in colors)
                palette.Add(color);
            return palette;
        }
    }

}
