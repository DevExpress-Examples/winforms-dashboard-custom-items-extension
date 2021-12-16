Namespace DevExpresss.DashboardWin.CustomItemExtension.CustomItems.WebPageItem
	Partial Public Class UriEditForm
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.layoutControl = New DevExpress.XtraLayout.LayoutControl()
			Me.lblUriPattern = New DevExpress.XtraEditors.LabelControl()
			Me.btnInsertPlaceholder = New DevExpress.XtraEditors.SimpleButton()
			Me.teUriPattern = New DevExpress.XtraEditors.TextEdit()
			Me.layoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
			Me.lciUriPattern = New DevExpress.XtraLayout.LayoutControlItem()
			Me.layoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
			Me.emptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
			Me.emptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
			Me.emptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem()
			Me.emptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem()
			Me.emptySpaceItem6 = New DevExpress.XtraLayout.EmptySpaceItem()
			Me.emptySpaceItem7 = New DevExpress.XtraLayout.EmptySpaceItem()
			Me.layoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
			Me.simpleButton1 = New DevExpress.XtraEditors.SimpleButton()
			Me.layoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
			Me.simpleButton2 = New DevExpress.XtraEditors.SimpleButton()
			Me.layoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
			Me.emptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
			CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.layoutControl.SuspendLayout()
			CType(Me.teUriPattern.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.layoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.lciUriPattern, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.layoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.emptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.emptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.emptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.emptySpaceItem5, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.emptySpaceItem6, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.emptySpaceItem7, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.layoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.layoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.layoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.emptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' layoutControl
			' 
			Me.layoutControl.AllowCustomization = False
			Me.layoutControl.Controls.Add(Me.simpleButton2)
			Me.layoutControl.Controls.Add(Me.simpleButton1)
			Me.layoutControl.Controls.Add(Me.lblUriPattern)
			Me.layoutControl.Controls.Add(Me.btnInsertPlaceholder)
			Me.layoutControl.Controls.Add(Me.teUriPattern)
			Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
			Me.layoutControl.Location = New System.Drawing.Point(0, 0)
			Me.layoutControl.Name = "layoutControl"
			Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(837, 120, 1067, 774)
			Me.layoutControl.Root = Me.layoutControlGroup1
			Me.layoutControl.Size = New System.Drawing.Size(427, 341)
			Me.layoutControl.TabIndex = 0
			' 
			' lblUriPattern
			' 
			Me.lblUriPattern.Location = New System.Drawing.Point(29, 49)
			Me.lblUriPattern.Name = "lblUriPattern"
			Me.lblUriPattern.Size = New System.Drawing.Size(91, 24)
			Me.lblUriPattern.StyleController = Me.layoutControl
			Me.lblUriPattern.TabIndex = 4
			Me.lblUriPattern.Text = "URI Pattern:"
			' 
			' btnInsertPlaceholder
			' 
			Me.btnInsertPlaceholder.Location = New System.Drawing.Point(123, 75)
			Me.btnInsertPlaceholder.Name = "btnInsertPlaceholder"
			Me.btnInsertPlaceholder.Size = New System.Drawing.Size(108, 22)
			Me.btnInsertPlaceholder.StyleController = Me.layoutControl
			Me.btnInsertPlaceholder.TabIndex = 5
			Me.btnInsertPlaceholder.Text = "Insert Placeholder"
			' 
			' teUriPattern
			' 
			Me.teUriPattern.Location = New System.Drawing.Point(122, 51)
			Me.teUriPattern.Name = "teUriPattern"
			Me.teUriPattern.Size = New System.Drawing.Size(293, 20)
			Me.teUriPattern.StyleController = Me.layoutControl
			Me.teUriPattern.TabIndex = 6
			' 
			' layoutControlGroup1
			' 
			Me.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True
			Me.layoutControlGroup1.GroupBordersVisible = False
			Me.layoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() { Me.lciUriPattern, Me.layoutControlItem4, Me.emptySpaceItem2, Me.emptySpaceItem3, Me.emptySpaceItem4, Me.emptySpaceItem5, Me.emptySpaceItem6, Me.emptySpaceItem7, Me.layoutControlItem3, Me.layoutControlItem1, Me.layoutControlItem2, Me.emptySpaceItem1})
			Me.layoutControlGroup1.Name = "Root"
			Me.layoutControlGroup1.Size = New System.Drawing.Size(427, 341)
			Me.layoutControlGroup1.TextVisible = False
			' 
			' lciUriPattern
			' 
			Me.lciUriPattern.Control = Me.teUriPattern
			Me.lciUriPattern.ControlAlignment = System.Drawing.ContentAlignment.MiddleRight
			Me.lciUriPattern.Location = New System.Drawing.Point(110, 39)
			Me.lciUriPattern.Name = "lciUriPattern"
			Me.lciUriPattern.Size = New System.Drawing.Size(297, 24)
			Me.lciUriPattern.TextSize = New System.Drawing.Size(0, 0)
			Me.lciUriPattern.TextVisible = False
			' 
			' layoutControlItem4
			' 
			Me.layoutControlItem4.Control = Me.btnInsertPlaceholder
			Me.layoutControlItem4.Location = New System.Drawing.Point(111, 63)
			Me.layoutControlItem4.Name = "layoutControlItem4"
			Me.layoutControlItem4.Size = New System.Drawing.Size(112, 26)
			Me.layoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
			Me.layoutControlItem4.TextVisible = False
			' 
			' emptySpaceItem2
			' 
			Me.emptySpaceItem2.AllowHotTrack = False
			Me.emptySpaceItem2.Location = New System.Drawing.Point(5, 89)
			Me.emptySpaceItem2.Name = "emptySpaceItem2"
			Me.emptySpaceItem2.Size = New System.Drawing.Size(402, 206)
			Me.emptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
			' 
			' emptySpaceItem3
			' 
			Me.emptySpaceItem3.AllowHotTrack = False
			Me.emptySpaceItem3.Location = New System.Drawing.Point(5, 63)
			Me.emptySpaceItem3.Name = "emptySpaceItem3"
			Me.emptySpaceItem3.Size = New System.Drawing.Size(106, 26)
			Me.emptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
			' 
			' emptySpaceItem4
			' 
			Me.emptySpaceItem4.AllowHotTrack = False
			Me.emptySpaceItem4.Location = New System.Drawing.Point(223, 63)
			Me.emptySpaceItem4.Name = "emptySpaceItem4"
			Me.emptySpaceItem4.Size = New System.Drawing.Size(184, 26)
			Me.emptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
			' 
			' emptySpaceItem5
			' 
			Me.emptySpaceItem5.AllowHotTrack = False
			Me.emptySpaceItem5.Location = New System.Drawing.Point(5, 39)
			Me.emptySpaceItem5.Name = "emptySpaceItem5"
			Me.emptySpaceItem5.Size = New System.Drawing.Size(14, 24)
			Me.emptySpaceItem5.TextSize = New System.Drawing.Size(0, 0)
			' 
			' emptySpaceItem6
			' 
			Me.emptySpaceItem6.AllowHotTrack = False
			Me.emptySpaceItem6.Location = New System.Drawing.Point(5, 0)
			Me.emptySpaceItem6.MinSize = New System.Drawing.Size(280, 22)
			Me.emptySpaceItem6.Name = "emptySpaceItem6"
			Me.emptySpaceItem6.Size = New System.Drawing.Size(402, 39)
			Me.emptySpaceItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
			Me.emptySpaceItem6.TextSize = New System.Drawing.Size(0, 0)
			' 
			' emptySpaceItem7
			' 
			Me.emptySpaceItem7.AllowHotTrack = False
			Me.emptySpaceItem7.Location = New System.Drawing.Point(0, 0)
			Me.emptySpaceItem7.MinSize = New System.Drawing.Size(4, 180)
			Me.emptySpaceItem7.Name = "emptySpaceItem7"
			Me.emptySpaceItem7.Size = New System.Drawing.Size(5, 321)
			Me.emptySpaceItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
			Me.emptySpaceItem7.TextSize = New System.Drawing.Size(0, 0)
			' 
			' layoutControlItem3
			' 
			Me.layoutControlItem3.Control = Me.lblUriPattern
			Me.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter
			Me.layoutControlItem3.Location = New System.Drawing.Point(19, 39)
			Me.layoutControlItem3.MinSize = New System.Drawing.Size(63, 13)
			Me.layoutControlItem3.Name = "layoutControlItem3"
			Me.layoutControlItem3.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
			Me.layoutControlItem3.Size = New System.Drawing.Size(91, 24)
			Me.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
			Me.layoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
			Me.layoutControlItem3.TextVisible = False
			' 
			' simpleButton1
			' 
			Me.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.OK
			Me.simpleButton1.Location = New System.Drawing.Point(339, 307)
			Me.simpleButton1.Name = "simpleButton1"
			Me.simpleButton1.Size = New System.Drawing.Size(76, 22)
			Me.simpleButton1.StyleController = Me.layoutControl
			Me.simpleButton1.TabIndex = 7
			Me.simpleButton1.Text = "Ok"
			' 
			' layoutControlItem1
			' 
			Me.layoutControlItem1.Control = Me.simpleButton1
			Me.layoutControlItem1.Location = New System.Drawing.Point(327, 295)
			Me.layoutControlItem1.MaxSize = New System.Drawing.Size(80, 26)
			Me.layoutControlItem1.MinSize = New System.Drawing.Size(80, 26)
			Me.layoutControlItem1.Name = "layoutControlItem1"
			Me.layoutControlItem1.Size = New System.Drawing.Size(80, 26)
			Me.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
			Me.layoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
			Me.layoutControlItem1.TextVisible = False
			' 
			' simpleButton2
			' 
			Me.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel
			Me.simpleButton2.Location = New System.Drawing.Point(259, 307)
			Me.simpleButton2.Name = "simpleButton2"
			Me.simpleButton2.Size = New System.Drawing.Size(76, 22)
			Me.simpleButton2.StyleController = Me.layoutControl
			Me.simpleButton2.TabIndex = 8
			Me.simpleButton2.Text = "Cancel"
			' 
			' layoutControlItem2
			' 
			Me.layoutControlItem2.Control = Me.simpleButton2
			Me.layoutControlItem2.Location = New System.Drawing.Point(247, 295)
			Me.layoutControlItem2.MaxSize = New System.Drawing.Size(80, 26)
			Me.layoutControlItem2.MinSize = New System.Drawing.Size(80, 26)
			Me.layoutControlItem2.Name = "layoutControlItem2"
			Me.layoutControlItem2.Size = New System.Drawing.Size(80, 26)
			Me.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
			Me.layoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
			Me.layoutControlItem2.TextVisible = False
			' 
			' emptySpaceItem1
			' 
			Me.emptySpaceItem1.AllowHotTrack = False
			Me.emptySpaceItem1.Location = New System.Drawing.Point(5, 295)
			Me.emptySpaceItem1.Name = "emptySpaceItem1"
			Me.emptySpaceItem1.Size = New System.Drawing.Size(242, 26)
			Me.emptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
			' 
			' UriEditForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(427, 341)
			Me.Controls.Add(Me.layoutControl)
			Me.MinimumSize = New System.Drawing.Size(300, 200)
			Me.Name = "UriEditForm"
			Me.Text = "Edit URI Settings"
			CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
			Me.layoutControl.ResumeLayout(False)
			CType(Me.teUriPattern.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.layoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.lciUriPattern, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.layoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.emptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.emptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.emptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.emptySpaceItem5, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.emptySpaceItem6, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.emptySpaceItem7, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.layoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.layoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.layoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.emptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub
		#End Region
		Private layoutControl As DevExpress.XtraLayout.LayoutControl
		Private lblUriPattern As DevExpress.XtraEditors.LabelControl
		Private WithEvents btnInsertPlaceholder As DevExpress.XtraEditors.SimpleButton
		Private teUriPattern As DevExpress.XtraEditors.TextEdit
		Private layoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
		Private lciUriPattern As DevExpress.XtraLayout.LayoutControlItem
		Private layoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
		Private emptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
		Private emptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
		Private emptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem
		Private emptySpaceItem6 As DevExpress.XtraLayout.EmptySpaceItem
		Private emptySpaceItem7 As DevExpress.XtraLayout.EmptySpaceItem
		Private layoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
		Private emptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
		Private simpleButton2 As DevExpress.XtraEditors.SimpleButton
		Private simpleButton1 As DevExpress.XtraEditors.SimpleButton
		Private layoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
		Private layoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
		Private emptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
	End Class
End Namespace