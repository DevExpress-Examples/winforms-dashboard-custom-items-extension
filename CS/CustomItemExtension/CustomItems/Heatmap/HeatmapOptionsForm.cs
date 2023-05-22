using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWin.Native;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpresss.DashboardWin.CustomItemExtension;

namespace DevExpresss.DashboardWin.CustomItemExtension.Heatmap {
	public partial class HeatmapOptionsForm : DashboardForm {
		public HeatmapOptionsForm() {
			InitializeComponent();
		}
		public HeatmapOptionsForm(MapPalette palette, MapScale scale)
			: this() {
			valueMapControl.InitializeFrom(palette, scale);
		}
		public MapScale GetScale() {
			return valueMapControl.GetScale();
		}

		public MapPalette GetPalette() {
			return valueMapControl.GetPalette();
		}
		void OnBtnOKClick(object sender, EventArgs e) {
			DialogResult = DialogResult.OK;
		}
		void OnBtnCancelClick(object sender, EventArgs e) {
			DialogResult = DialogResult.Cancel;
		}
	}
}
