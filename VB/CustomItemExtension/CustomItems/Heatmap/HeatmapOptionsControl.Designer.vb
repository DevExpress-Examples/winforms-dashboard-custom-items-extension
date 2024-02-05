Namespace DevExpress.DashboardWin.CustomItemExtension.Heatmap
	Partial Public Class HeatmapOptionsControl
		Private components As System.ComponentModel.IContainer = Nothing
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
		#Region "Component Designer generated code"
		Private Sub InitializeComponent()
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(HeatmapOptionsControl))
			Me.repositoryItemColorEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemColorEdit()
			Me.colorEdit2 = New DevExpress.XtraEditors.ColorEdit()
			Me.layoutControl1 = New DevExpress.XtraLayout.LayoutControl()
			Me.gridControl = New DevExpress.XtraGrid.GridControl()
			Me.gridView = New DevExpress.XtraGrid.Views.Grid.GridView()
			Me.gridColumnRangeStop = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.gridColumnColor = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.allowEditCheckEdit = New DevExpress.XtraEditors.CheckEdit()
			Me.absoluteLevelsEdit = New DevExpress.XtraEditors.SpinEdit()
			Me.absoluteScaleCheckEdit = New DevExpress.XtraEditors.CheckEdit()
			Me.percentLevelsEdit = New DevExpress.XtraEditors.SpinEdit()
			Me.colorEdit1 = New DevExpress.XtraEditors.ColorEdit()
			Me.percentsCheckEdit = New DevExpress.XtraEditors.CheckEdit()
			Me.autoColorsCheckEdit = New DevExpress.XtraEditors.CheckEdit()
			Me.customColorsCheckEdit = New DevExpress.XtraEditors.CheckEdit()
			Me.layoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
			Me.groupPalette = New DevExpress.XtraLayout.LayoutControlGroup()
			Me.layoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
			Me.labelControl1 = New DevExpress.XtraLayout.LayoutControlItem()
			Me.labelControl2 = New DevExpress.XtraLayout.LayoutControlItem()
			Me.layoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
			Me.emptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
			Me.emptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem()
			Me.emptySpaceItem6 = New DevExpress.XtraLayout.EmptySpaceItem()
			Me.scaleContainer = New DevExpress.XtraLayout.LayoutControlGroup()
			Me.layoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
			Me.labelThresholdType = New DevExpress.XtraLayout.LayoutControlItem()
			Me.labelControl3 = New DevExpress.XtraLayout.LayoutControlItem()
			Me.layoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
			Me.emptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
			Me.emptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
			Me.emptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem()
			Me.groupControl1 = New DevExpress.XtraLayout.LayoutControlGroup()
			Me.layoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
			Me.layoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
			DirectCast(Me.repositoryItemColorEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.colorEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.layoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.layoutControl1.SuspendLayout()
			DirectCast(Me.gridControl, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.gridView, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.allowEditCheckEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.absoluteLevelsEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.absoluteScaleCheckEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.percentLevelsEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.colorEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.percentsCheckEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.autoColorsCheckEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.customColorsCheckEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.layoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.groupPalette, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.layoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.labelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.labelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.layoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.emptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.emptySpaceItem5, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.emptySpaceItem6, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.scaleContainer, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.layoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.labelThresholdType, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.labelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.layoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.emptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.emptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.emptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.groupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.layoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.layoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' repositoryItemColorEdit1
			' 
			resources.ApplyResources(Me.repositoryItemColorEdit1, "repositoryItemColorEdit1")
			Me.repositoryItemColorEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton((DirectCast(resources.GetObject("repositoryItemColorEdit1.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines)))})
			Me.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1"
			' 
			' colorEdit2
			' 
			resources.ApplyResources(Me.colorEdit2, "colorEdit2")
			Me.colorEdit2.Name = "colorEdit2"
			Me.colorEdit2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton((DirectCast(resources.GetObject("colorEdit2.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines)))})
			Me.colorEdit2.StyleController = Me.layoutControl1
			' 
			' layoutControl1
			' 
			Me.layoutControl1.AllowCustomization = False
			resources.ApplyResources(Me.layoutControl1, "layoutControl1")
			Me.layoutControl1.Controls.Add(Me.gridControl)
			Me.layoutControl1.Controls.Add(Me.allowEditCheckEdit)
			Me.layoutControl1.Controls.Add(Me.absoluteLevelsEdit)
			Me.layoutControl1.Controls.Add(Me.absoluteScaleCheckEdit)
			Me.layoutControl1.Controls.Add(Me.percentLevelsEdit)
			Me.layoutControl1.Controls.Add(Me.colorEdit2)
			Me.layoutControl1.Controls.Add(Me.colorEdit1)
			Me.layoutControl1.Controls.Add(Me.percentsCheckEdit)
			Me.layoutControl1.Controls.Add(Me.autoColorsCheckEdit)
			Me.layoutControl1.Controls.Add(Me.customColorsCheckEdit)
			Me.layoutControl1.Name = "layoutControl1"
			Me.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(796, 325, 650, 400)
			Me.layoutControl1.OptionsFocus.AllowFocusReadonlyEditors = False
			Me.layoutControl1.OptionsFocus.EnableAutoTabOrder = False
			Me.layoutControl1.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignMode.AlignInGroups
			Me.layoutControl1.Root = Me.layoutControlGroup1
			' 
			' gridControl
			' 
			Me.gridControl.EmbeddedNavigator.Buttons.Edit.Visible = False
			Me.gridControl.EmbeddedNavigator.Buttons.First.Visible = False
			Me.gridControl.EmbeddedNavigator.Buttons.Last.Visible = False
			Me.gridControl.EmbeddedNavigator.Buttons.Next.Visible = False
			Me.gridControl.EmbeddedNavigator.Buttons.NextPage.Visible = False
			Me.gridControl.EmbeddedNavigator.Buttons.Prev.Visible = False
			Me.gridControl.EmbeddedNavigator.Buttons.PrevPage.Enabled = False
			Me.gridControl.EmbeddedNavigator.Buttons.PrevPage.Visible = False
			Me.gridControl.EmbeddedNavigator.Cursor = System.Windows.Forms.Cursors.Arrow
			Me.gridControl.EmbeddedNavigator.TextLocation = (DirectCast(resources.GetObject("gridControl.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation))
			resources.ApplyResources(Me.gridControl, "gridControl")
			Me.gridControl.MainView = Me.gridView
			Me.gridControl.Name = "gridControl"
			Me.gridControl.UseEmbeddedNavigator = True
			Me.gridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridView})
			' 
			' gridView
			' 
			Me.gridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
			Me.gridView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() { Me.gridColumnRangeStop, Me.gridColumnColor})
			Me.gridView.GridControl = Me.gridControl
			Me.gridView.Name = "gridView"
			Me.gridView.OptionsDetail.EnableMasterViewMode = False
			Me.gridView.OptionsFilter.AllowFilterEditor = False
			Me.gridView.OptionsMenu.EnableColumnMenu = False
			Me.gridView.OptionsSelection.EnableAppearanceFocusedRow = False
			Me.gridView.OptionsView.BestFitMaxRowCount = 1000
			Me.gridView.OptionsView.ColumnAutoWidth = False
			Me.gridView.OptionsView.ShowGroupPanel = False
			' 
			' gridColumnRangeStop
			' 
			resources.ApplyResources(Me.gridColumnRangeStop, "gridColumnRangeStop")
			Me.gridColumnRangeStop.FieldName = "Range"
			Me.gridColumnRangeStop.Name = "gridColumnRangeStop"
			' 
			' gridColumnColor
			' 
			resources.ApplyResources(Me.gridColumnColor, "gridColumnColor")
			Me.gridColumnColor.ColumnEdit = Me.repositoryItemColorEdit1
			Me.gridColumnColor.FieldName = "Color"
			Me.gridColumnColor.Name = "gridColumnColor"
			' 
			' allowEditCheckEdit
			' 
			resources.ApplyResources(Me.allowEditCheckEdit, "allowEditCheckEdit")
			Me.allowEditCheckEdit.Name = "allowEditCheckEdit"
			Me.allowEditCheckEdit.Properties.Caption = resources.GetString("allowEditCheckEdit.Properties.Caption")
			Me.allowEditCheckEdit.StyleController = Me.layoutControl1
			' 
			' absoluteLevelsEdit
			' 
			resources.ApplyResources(Me.absoluteLevelsEdit, "absoluteLevelsEdit")
			Me.absoluteLevelsEdit.Name = "absoluteLevelsEdit"
			Me.absoluteLevelsEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton((DirectCast(resources.GetObject("absoluteLevelsEdit.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines)))})
			Me.absoluteLevelsEdit.StyleController = Me.layoutControl1
			' 
			' absoluteScaleCheckEdit
			' 
			resources.ApplyResources(Me.absoluteScaleCheckEdit, "absoluteScaleCheckEdit")
			Me.absoluteScaleCheckEdit.Name = "absoluteScaleCheckEdit"
			Me.absoluteScaleCheckEdit.Properties.Caption = resources.GetString("absoluteScaleCheckEdit.Properties.Caption")
			Me.absoluteScaleCheckEdit.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
			Me.absoluteScaleCheckEdit.Properties.RadioGroupIndex = 0
			Me.absoluteScaleCheckEdit.StyleController = Me.layoutControl1
			Me.absoluteScaleCheckEdit.TabStop = False
			' 
			' percentLevelsEdit
			' 
			resources.ApplyResources(Me.percentLevelsEdit, "percentLevelsEdit")
			Me.percentLevelsEdit.Name = "percentLevelsEdit"
			Me.percentLevelsEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton((DirectCast(resources.GetObject("percentLevelsEdit.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines)))})
			Me.percentLevelsEdit.StyleController = Me.layoutControl1
			' 
			' colorEdit1
			' 
			resources.ApplyResources(Me.colorEdit1, "colorEdit1")
			Me.colorEdit1.Name = "colorEdit1"
			Me.colorEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton((DirectCast(resources.GetObject("colorEdit1.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines)))})
			Me.colorEdit1.StyleController = Me.layoutControl1
			' 
			' percentsCheckEdit
			' 
			resources.ApplyResources(Me.percentsCheckEdit, "percentsCheckEdit")
			Me.percentsCheckEdit.Name = "percentsCheckEdit"
			Me.percentsCheckEdit.Properties.Caption = resources.GetString("percentsCheckEdit.Properties.Caption")
			Me.percentsCheckEdit.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
			Me.percentsCheckEdit.Properties.RadioGroupIndex = 0
			Me.percentsCheckEdit.StyleController = Me.layoutControl1
			' 
			' autoColorsCheckEdit
			' 
			resources.ApplyResources(Me.autoColorsCheckEdit, "autoColorsCheckEdit")
			Me.autoColorsCheckEdit.Name = "autoColorsCheckEdit"
			Me.autoColorsCheckEdit.Properties.Caption = resources.GetString("autoColorsCheckEdit.Properties.Caption")
			Me.autoColorsCheckEdit.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
			Me.autoColorsCheckEdit.Properties.RadioGroupIndex = 1
			Me.autoColorsCheckEdit.StyleController = Me.layoutControl1
			' 
			' customColorsCheckEdit
			' 
			resources.ApplyResources(Me.customColorsCheckEdit, "customColorsCheckEdit")
			Me.customColorsCheckEdit.Name = "customColorsCheckEdit"
			Me.customColorsCheckEdit.Properties.Caption = resources.GetString("customColorsCheckEdit.Properties.Caption")
			Me.customColorsCheckEdit.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
			Me.customColorsCheckEdit.Properties.RadioGroupIndex = 1
			Me.customColorsCheckEdit.StyleController = Me.layoutControl1
			Me.customColorsCheckEdit.TabStop = False
			' 
			' layoutControlGroup1
			' 
			Me.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True
			Me.layoutControlGroup1.GroupBordersVisible = False
			Me.layoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() { Me.groupPalette, Me.scaleContainer, Me.groupControl1})
			Me.layoutControlGroup1.Name = "Root"
			Me.layoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2)
			Me.layoutControlGroup1.Size = New System.Drawing.Size(585, 304)
			Me.layoutControlGroup1.TextVisible = False
			' 
			' groupPalette
			' 
			Me.groupPalette.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() { Me.layoutControlItem2, Me.labelControl1, Me.labelControl2, Me.layoutControlItem1, Me.emptySpaceItem1, Me.emptySpaceItem5, Me.emptySpaceItem6})
			Me.groupPalette.Location = New System.Drawing.Point(0, 0)
			Me.groupPalette.Name = "groupPalette"
			Me.groupPalette.Size = New System.Drawing.Size(282, 150)
			Me.groupPalette.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 3, 0, 3)
			resources.ApplyResources(Me.groupPalette, "groupPalette")
			' 
			' layoutControlItem2
			' 
			Me.layoutControlItem2.Control = Me.customColorsCheckEdit
			Me.layoutControlItem2.Location = New System.Drawing.Point(0, 34)
			Me.layoutControlItem2.Name = "layoutControlItem2"
			Me.layoutControlItem2.Size = New System.Drawing.Size(259, 24)
			Me.layoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
			Me.layoutControlItem2.TextVisible = False
			' 
			' labelControl1
			' 
			Me.labelControl1.Control = Me.colorEdit1
			Me.labelControl1.Location = New System.Drawing.Point(54, 58)
			Me.labelControl1.Name = "labelControl1"
			Me.labelControl1.Size = New System.Drawing.Size(205, 24)
			resources.ApplyResources(Me.labelControl1, "labelControl1")
			Me.labelControl1.TextSize = New System.Drawing.Size(50, 13)
			' 
			' labelControl2
			' 
			Me.labelControl2.Control = Me.colorEdit2
			Me.labelControl2.Location = New System.Drawing.Point(54, 82)
			Me.labelControl2.Name = "labelControl2"
			Me.labelControl2.Size = New System.Drawing.Size(205, 24)
			resources.ApplyResources(Me.labelControl2, "labelControl2")
			Me.labelControl2.TextSize = New System.Drawing.Size(50, 13)
			' 
			' layoutControlItem1
			' 
			Me.layoutControlItem1.Control = Me.autoColorsCheckEdit
			Me.layoutControlItem1.Location = New System.Drawing.Point(0, 0)
			Me.layoutControlItem1.Name = "layoutControlItem1"
			Me.layoutControlItem1.Size = New System.Drawing.Size(259, 24)
			Me.layoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
			Me.layoutControlItem1.TextVisible = False
			' 
			' emptySpaceItem1
			' 
			Me.emptySpaceItem1.AllowHotTrack = False
			Me.emptySpaceItem1.Location = New System.Drawing.Point(0, 24)
			Me.emptySpaceItem1.Name = "emptySpaceItem1"
			Me.emptySpaceItem1.Size = New System.Drawing.Size(259, 10)
			Me.emptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
			' 
			' emptySpaceItem5
			' 
			Me.emptySpaceItem5.AllowHotTrack = False
			Me.emptySpaceItem5.Location = New System.Drawing.Point(0, 58)
			Me.emptySpaceItem5.MinSize = New System.Drawing.Size(51, 10)
			Me.emptySpaceItem5.Name = "emptySpaceItem5"
			Me.emptySpaceItem5.Size = New System.Drawing.Size(54, 24)
			Me.emptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
			Me.emptySpaceItem5.TextSize = New System.Drawing.Size(0, 0)
			' 
			' emptySpaceItem6
			' 
			Me.emptySpaceItem6.AllowHotTrack = False
			Me.emptySpaceItem6.Location = New System.Drawing.Point(0, 82)
			Me.emptySpaceItem6.MinSize = New System.Drawing.Size(51, 24)
			Me.emptySpaceItem6.Name = "emptySpaceItem6"
			Me.emptySpaceItem6.Size = New System.Drawing.Size(54, 24)
			Me.emptySpaceItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
			Me.emptySpaceItem6.TextSize = New System.Drawing.Size(0, 0)
			' 
			' scaleContainer
			' 
			Me.scaleContainer.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() { Me.layoutControlItem3, Me.labelThresholdType, Me.labelControl3, Me.layoutControlItem6, Me.emptySpaceItem2, Me.emptySpaceItem3, Me.emptySpaceItem4})
			Me.scaleContainer.Location = New System.Drawing.Point(0, 150)
			Me.scaleContainer.Name = "scaleContainer"
			Me.scaleContainer.Size = New System.Drawing.Size(282, 150)
			Me.scaleContainer.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 3, 3, 0)
			resources.ApplyResources(Me.scaleContainer, "scaleContainer")
			' 
			' layoutControlItem3
			' 
			Me.layoutControlItem3.Control = Me.percentsCheckEdit
			Me.layoutControlItem3.Location = New System.Drawing.Point(0, 0)
			Me.layoutControlItem3.Name = "layoutControlItem3"
			Me.layoutControlItem3.Size = New System.Drawing.Size(259, 24)
			Me.layoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
			Me.layoutControlItem3.TextVisible = False
			' 
			' labelThresholdType
			' 
			Me.labelThresholdType.Control = Me.percentLevelsEdit
			Me.labelThresholdType.Location = New System.Drawing.Point(19, 24)
			Me.labelThresholdType.Name = "labelThresholdType"
			Me.labelThresholdType.Size = New System.Drawing.Size(240, 24)
			resources.ApplyResources(Me.labelThresholdType, "labelThresholdType")
			Me.labelThresholdType.TextSize = New System.Drawing.Size(84, 13)
			' 
			' labelControl3
			' 
			Me.labelControl3.Control = Me.absoluteLevelsEdit
			Me.labelControl3.Location = New System.Drawing.Point(19, 82)
			Me.labelControl3.Name = "labelControl3"
			Me.labelControl3.Size = New System.Drawing.Size(240, 24)
			resources.ApplyResources(Me.labelControl3, "labelControl3")
			Me.labelControl3.TextSize = New System.Drawing.Size(84, 13)
			' 
			' layoutControlItem6
			' 
			Me.layoutControlItem6.Control = Me.absoluteScaleCheckEdit
			Me.layoutControlItem6.Location = New System.Drawing.Point(0, 58)
			Me.layoutControlItem6.Name = "layoutControlItem6"
			Me.layoutControlItem6.Size = New System.Drawing.Size(259, 24)
			Me.layoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
			Me.layoutControlItem6.TextVisible = False
			' 
			' emptySpaceItem2
			' 
			Me.emptySpaceItem2.AllowHotTrack = False
			Me.emptySpaceItem2.Location = New System.Drawing.Point(0, 48)
			Me.emptySpaceItem2.Name = "emptySpaceItem2"
			Me.emptySpaceItem2.Size = New System.Drawing.Size(259, 10)
			Me.emptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
			' 
			' emptySpaceItem3
			' 
			Me.emptySpaceItem3.AllowHotTrack = False
			Me.emptySpaceItem3.Location = New System.Drawing.Point(0, 24)
			Me.emptySpaceItem3.MinSize = New System.Drawing.Size(19, 10)
			Me.emptySpaceItem3.Name = "emptySpaceItem3"
			Me.emptySpaceItem3.Size = New System.Drawing.Size(19, 24)
			Me.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
			Me.emptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
			' 
			' emptySpaceItem4
			' 
			Me.emptySpaceItem4.AllowHotTrack = False
			Me.emptySpaceItem4.Location = New System.Drawing.Point(0, 82)
			Me.emptySpaceItem4.MinSize = New System.Drawing.Size(19, 10)
			Me.emptySpaceItem4.Name = "emptySpaceItem4"
			Me.emptySpaceItem4.Size = New System.Drawing.Size(19, 24)
			Me.emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
			Me.emptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
			' 
			' groupControl1
			' 
			Me.groupControl1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() { Me.layoutControlItem4, Me.layoutControlItem5})
			Me.groupControl1.Location = New System.Drawing.Point(282, 0)
			Me.groupControl1.Name = "groupControl1"
			Me.groupControl1.Size = New System.Drawing.Size(299, 300)
			Me.groupControl1.Spacing = New DevExpress.XtraLayout.Utils.Padding(3, 0, 0, 0)
			resources.ApplyResources(Me.groupControl1, "groupControl1")
			' 
			' layoutControlItem4
			' 
			Me.layoutControlItem4.Control = Me.allowEditCheckEdit
			Me.layoutControlItem4.Location = New System.Drawing.Point(0, 0)
			Me.layoutControlItem4.Name = "layoutControlItem4"
			Me.layoutControlItem4.Size = New System.Drawing.Size(276, 24)
			Me.layoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
			Me.layoutControlItem4.TextVisible = False
			' 
			' layoutControlItem5
			' 
			Me.layoutControlItem5.Control = Me.gridControl
			Me.layoutControlItem5.Location = New System.Drawing.Point(0, 24)
			Me.layoutControlItem5.Name = "layoutControlItem5"
			Me.layoutControlItem5.Size = New System.Drawing.Size(276, 235)
			Me.layoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
			Me.layoutControlItem5.TextVisible = False
			' 
			' HeatmapOptionsControl
			' 
			resources.ApplyResources(Me, "$this")
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.layoutControl1)
			Me.Name = "HeatmapOptionsControl"
			DirectCast(Me.repositoryItemColorEdit1, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.colorEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.layoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.layoutControl1.ResumeLayout(False)
			DirectCast(Me.gridControl, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.gridView, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.allowEditCheckEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.absoluteLevelsEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.absoluteScaleCheckEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.percentLevelsEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.colorEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.percentsCheckEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.autoColorsCheckEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.customColorsCheckEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.layoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.groupPalette, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.layoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.labelControl1, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.labelControl2, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.layoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.emptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.emptySpaceItem5, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.emptySpaceItem6, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.scaleContainer, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.layoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.labelThresholdType, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.labelControl3, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.layoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.emptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.emptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.emptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.groupControl1, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.layoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.layoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub
		#End Region
		Private WithEvents colorEdit2 As DevExpress.XtraEditors.ColorEdit
		Private WithEvents colorEdit1 As DevExpress.XtraEditors.ColorEdit
		Private WithEvents percentLevelsEdit As DevExpress.XtraEditors.SpinEdit
		Private WithEvents customColorsCheckEdit As DevExpress.XtraEditors.CheckEdit
		Private gridControl As DevExpress.XtraGrid.GridControl
		Private gridView As DevExpress.XtraGrid.Views.Grid.GridView
		Private gridColumnRangeStop As DevExpress.XtraGrid.Columns.GridColumn
		Private gridColumnColor As DevExpress.XtraGrid.Columns.GridColumn
		Private WithEvents percentsCheckEdit As DevExpress.XtraEditors.CheckEdit
		Private autoColorsCheckEdit As DevExpress.XtraEditors.CheckEdit
		Private WithEvents allowEditCheckEdit As DevExpress.XtraEditors.CheckEdit
		Private WithEvents absoluteLevelsEdit As DevExpress.XtraEditors.SpinEdit
		Private WithEvents absoluteScaleCheckEdit As DevExpress.XtraEditors.CheckEdit
		Private repositoryItemColorEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemColorEdit
		Private layoutControl1 As DevExpress.XtraLayout.LayoutControl
		Private layoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
		Private layoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
		Private emptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
		Private layoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
		Private labelControl1 As DevExpress.XtraLayout.LayoutControlItem
		Private labelControl2 As DevExpress.XtraLayout.LayoutControlItem
		Private groupPalette As DevExpress.XtraLayout.LayoutControlGroup
		Private emptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem
		Private emptySpaceItem6 As DevExpress.XtraLayout.EmptySpaceItem
		Private scaleContainer As DevExpress.XtraLayout.LayoutControlGroup
		Private layoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
		Private labelThresholdType As DevExpress.XtraLayout.LayoutControlItem
		Private labelControl3 As DevExpress.XtraLayout.LayoutControlItem
		Private layoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
		Private emptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
		Private emptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
		Private emptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
		Private groupControl1 As DevExpress.XtraLayout.LayoutControlGroup
		Private layoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
		Private layoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
	End Class
End Namespace
