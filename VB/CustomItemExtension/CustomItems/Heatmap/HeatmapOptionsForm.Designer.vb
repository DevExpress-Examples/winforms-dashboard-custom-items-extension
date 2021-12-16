Imports DevExpress.DashboardWin.Native

Namespace DevExpresss.DashboardWin.CustomItemExtension.Heatmap
	Partial Public Class HeatmapOptionsForm
		Private components As System.ComponentModel.IContainer = Nothing
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
		#Region "Windows Form Designer generated code"
		Private Sub InitializeComponent()
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(HeatmapOptionsForm))
			Me.separator = New DevExpress.XtraEditors.LabelControl()
			Me.layoutControl1 = New DevExpress.XtraLayout.LayoutControl()
			Me.valueMapControl = New DevExpresss.DashboardWin.CustomItemExtension.Heatmap.HeatmapOptionsControl()
			Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
			Me.btnOK = New DevExpress.XtraEditors.SimpleButton()
			Me.layoutControlGroup8 = New DevExpress.XtraLayout.LayoutControlGroup()
			Me.layoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
			Me.layoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
			Me.layoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
			Me.emptySpaceItem9 = New DevExpress.XtraLayout.EmptySpaceItem()
			Me.layoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
			Me.emptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
			Me.lcgValueMap = New DevExpress.XtraLayout.LayoutControlGroup()
			Me.lciValueMapControl = New DevExpress.XtraLayout.LayoutControlItem()
			Me.emptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
			DirectCast(Me.layoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.layoutControl1.SuspendLayout()
			DirectCast(Me.layoutControlGroup8, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.layoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.layoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.layoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.emptySpaceItem9, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.layoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.emptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.lcgValueMap, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.lciValueMapControl, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.emptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' separator
			' 
			Me.separator.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Vertical
			Me.separator.LineVisible = True
			resources.ApplyResources(Me.separator, "separator")
			Me.separator.Name = "separator"
			' 
			' layoutControl1
			' 
			Me.layoutControl1.AllowCustomization = False
			resources.ApplyResources(Me.layoutControl1, "layoutControl1")
			Me.layoutControl1.Controls.Add(Me.valueMapControl)
			Me.layoutControl1.Controls.Add(Me.btnCancel)
			Me.layoutControl1.Controls.Add(Me.btnOK)
			Me.layoutControl1.HiddenItems.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() { Me.layoutControlGroup8})
			Me.layoutControl1.Name = "layoutControl1"
			Me.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(913, 143, 822, 812)
			Me.layoutControl1.OptionsFocus.AllowFocusReadonlyEditors = False
			Me.layoutControl1.OptionsFocus.AllowFocusTabbedGroups = False
			Me.layoutControl1.OptionsFocus.EnableAutoTabOrder = False
			Me.layoutControl1.Root = Me.layoutControlGroup1
			' 
			' valueMapControl
			' 
			resources.ApplyResources(Me.valueMapControl, "valueMapControl")
			Me.valueMapControl.Name = "valueMapControl"
			' 
			' btnCancel
			' 
			resources.ApplyResources(Me.btnCancel, "btnCancel")
			Me.btnCancel.AutoWidthInLayoutControl = True
			Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
			Me.btnCancel.Name = "btnCancel"
			Me.btnCancel.StyleController = Me.layoutControl1
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnCancel.Click += new System.EventHandler(this.OnBtnCancelClick);
			' 
			' btnOK
			' 
			resources.ApplyResources(Me.btnOK, "btnOK")
			Me.btnOK.AutoWidthInLayoutControl = True
			Me.btnOK.Name = "btnOK"
			Me.btnOK.StyleController = Me.layoutControl1
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.btnOK.Click += new System.EventHandler(this.OnBtnOKClick);
			' 
			' layoutControlGroup8
			' 
			Me.layoutControlGroup8.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False
			Me.layoutControlGroup8.GroupBordersVisible = False
			Me.layoutControlGroup8.Location = New System.Drawing.Point(0, 0)
			Me.layoutControlGroup8.Name = "layoutControlGroup8"
			Me.layoutControlGroup8.Size = New System.Drawing.Size(125, 354)
			Me.layoutControlGroup8.TextVisible = False
			' 
			' layoutControlGroup1
			' 
			Me.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True
			Me.layoutControlGroup1.GroupBordersVisible = False
			Me.layoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() { Me.layoutControlGroup2, Me.emptySpaceItem3, Me.lcgValueMap})
			Me.layoutControlGroup1.Name = "Root"
			Me.layoutControlGroup1.Size = New System.Drawing.Size(609, 374)
			Me.layoutControlGroup1.TextVisible = False
			' 
			' layoutControlGroup2
			' 
			Me.layoutControlGroup2.GroupBordersVisible = False
			Me.layoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() { Me.layoutControlItem6, Me.emptySpaceItem9, Me.layoutControlItem5})
			Me.layoutControlGroup2.Location = New System.Drawing.Point(0, 328)
			Me.layoutControlGroup2.Name = "layoutControlGroup2"
			Me.layoutControlGroup2.Size = New System.Drawing.Size(589, 26)
			' 
			' layoutControlItem6
			' 
			Me.layoutControlItem6.Control = Me.btnCancel
			Me.layoutControlItem6.FillControlToClientArea = False
			Me.layoutControlItem6.Location = New System.Drawing.Point(508, 0)
			Me.layoutControlItem6.Name = "layoutControlItem6"
			Me.layoutControlItem6.Padding = New DevExpress.XtraLayout.Utils.Padding(3, 3, 2, 2)
			Me.layoutControlItem6.Size = New System.Drawing.Size(81, 26)
			Me.layoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
			Me.layoutControlItem6.TextVisible = False
			' 
			' emptySpaceItem9
			' 
			Me.emptySpaceItem9.AllowHotTrack = False
			Me.emptySpaceItem9.Location = New System.Drawing.Point(0, 0)
			Me.emptySpaceItem9.Name = "emptySpaceItem9"
			Me.emptySpaceItem9.Size = New System.Drawing.Size(429, 26)
			Me.emptySpaceItem9.TextSize = New System.Drawing.Size(0, 0)
			' 
			' layoutControlItem5
			' 
			Me.layoutControlItem5.Control = Me.btnOK
			Me.layoutControlItem5.FillControlToClientArea = False
			Me.layoutControlItem5.Location = New System.Drawing.Point(429, 0)
			Me.layoutControlItem5.Name = "layoutControlItem5"
			Me.layoutControlItem5.Size = New System.Drawing.Size(79, 26)
			Me.layoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
			Me.layoutControlItem5.TextVisible = False
			' 
			' emptySpaceItem3
			' 
			Me.emptySpaceItem3.AllowHotTrack = False
			Me.emptySpaceItem3.Location = New System.Drawing.Point(0, 318)
			Me.emptySpaceItem3.Name = "emptySpaceItem3"
			Me.emptySpaceItem3.Size = New System.Drawing.Size(589, 10)
			Me.emptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
			' 
			' lcgValueMap
			' 
			Me.lcgValueMap.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False
			Me.lcgValueMap.GroupBordersVisible = False
			Me.lcgValueMap.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() { Me.lciValueMapControl, Me.emptySpaceItem1})
			Me.lcgValueMap.Location = New System.Drawing.Point(0, 0)
			Me.lcgValueMap.Name = "lcgValueMap"
			Me.lcgValueMap.Size = New System.Drawing.Size(589, 318)
			Me.lcgValueMap.TextVisible = False
			' 
			' lciValueMapControl
			' 
			Me.lciValueMapControl.Control = Me.valueMapControl
			Me.lciValueMapControl.Location = New System.Drawing.Point(0, 0)
			Me.lciValueMapControl.Name = "lciValueMapControl"
			Me.lciValueMapControl.Size = New System.Drawing.Size(589, 308)
			Me.lciValueMapControl.TextSize = New System.Drawing.Size(0, 0)
			Me.lciValueMapControl.TextVisible = False
			' 
			' emptySpaceItem1
			' 
			Me.emptySpaceItem1.AllowHotTrack = False
			Me.emptySpaceItem1.Location = New System.Drawing.Point(0, 308)
			Me.emptySpaceItem1.Name = "emptySpaceItem1"
			Me.emptySpaceItem1.Size = New System.Drawing.Size(589, 10)
			Me.emptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
			' 
			' HeatmapOptionsForm
			' 
			Me.AcceptButton = Me.btnOK
			resources.ApplyResources(Me, "$this")
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.CancelButton = Me.btnCancel
			Me.Controls.Add(Me.layoutControl1)
			Me.Controls.Add(Me.separator)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
			Me.IconOptions.ShowIcon = False
			Me.MaximizeBox = False
			Me.MinimizeBox = False
			Me.Name = "HeatmapOptionsForm"
			DirectCast(Me.layoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.layoutControl1.ResumeLayout(False)
			DirectCast(Me.layoutControlGroup8, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.layoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.layoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.layoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.emptySpaceItem9, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.layoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.emptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.lcgValueMap, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.lciValueMapControl, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.emptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub
		#End Region
		Private separator As DevExpress.XtraEditors.LabelControl
		Private WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
		Private WithEvents btnOK As DevExpress.XtraEditors.SimpleButton
		Private valueMapControl As HeatmapOptionsControl
		Private layoutControl1 As DevExpress.XtraLayout.LayoutControl
		Private layoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
		Private layoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
		Private layoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
		Private layoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
		Private emptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
		Private emptySpaceItem9 As DevExpress.XtraLayout.EmptySpaceItem
		Private layoutControlGroup8 As DevExpress.XtraLayout.LayoutControlGroup
		Private lciValueMapControl As DevExpress.XtraLayout.LayoutControlItem
		Private emptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
		Private lcgValueMap As DevExpress.XtraLayout.LayoutControlGroup
	End Class
End Namespace
