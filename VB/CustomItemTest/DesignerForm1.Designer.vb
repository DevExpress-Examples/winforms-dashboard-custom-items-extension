Namespace CustomItemTest
	Partial Public Class DesignerForm1
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
            Me.components = New System.ComponentModel.Container()
            Me.dashboardBarAndDockingController1 = New DevExpress.DashboardWin.Native.DashboardBarAndDockingController(Me.components)
            Me.dashboardDesigner = New DevExpress.DashboardWin.DashboardDesigner()
            Me.dashboardsAccordion = New DevExpress.XtraBars.Navigation.AccordionControl()
            CType(Me.dashboardBarAndDockingController1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dashboardDesigner, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dashboardsAccordion, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'dashboardBarAndDockingController1
            '
            Me.dashboardBarAndDockingController1.PropertiesDocking.ViewStyle = DevExpress.XtraBars.Docking2010.Views.DockingViewStyle.Classic
            '
            'dashboardDesigner
            '
            Me.dashboardDesigner.AsyncMode = True
            Me.dashboardDesigner.BarAndDockingController = Me.dashboardBarAndDockingController1
            Me.dashboardDesigner.DataSourceWizard.ShowConnectionsFromAppConfig = True
            Me.dashboardDesigner.DataSourceWizard.SqlWizardSettings.DatabaseCredentialsSavingBehavior = DevExpress.DataAccess.Wizard.SensitiveInfoSavingBehavior.Prompt
            Me.dashboardDesigner.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dashboardDesigner.Location = New System.Drawing.Point(227, 0)
            Me.dashboardDesigner.Name = "dashboardDesigner"
            Me.dashboardDesigner.Size = New System.Drawing.Size(1315, 778)
            Me.dashboardDesigner.TabIndex = 0
            Me.dashboardDesigner.UseNeutralFilterMode = True
            '
            'dashboardsAccordion
            '
            Me.dashboardsAccordion.AllowItemSelection = True
            Me.dashboardsAccordion.Dock = System.Windows.Forms.DockStyle.Left
            Me.dashboardsAccordion.Location = New System.Drawing.Point(0, 0)
            Me.dashboardsAccordion.Name = "dashboardsAccordion"
            Me.dashboardsAccordion.Size = New System.Drawing.Size(227, 778)
            Me.dashboardsAccordion.TabIndex = 1
            Me.dashboardsAccordion.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu
            '
            'DesignerForm1
            '
            Me.AllowFormGlass = DevExpress.Utils.DefaultBoolean.[False]
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1542, 778)
            Me.Controls.Add(Me.dashboardDesigner)
            Me.Controls.Add(Me.dashboardsAccordion)
            Me.Name = "DesignerForm1"
            Me.Text = "Dashboard Designer"
            CType(Me.dashboardBarAndDockingController1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.dashboardDesigner, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.dashboardsAccordion, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

#End Region

        Private dashboardDesigner As DevExpress.DashboardWin.DashboardDesigner
		Private dashboardBarAndDockingController1 As DevExpress.DashboardWin.Native.DashboardBarAndDockingController
		Private WithEvents dashboardsAccordion As DevExpress.XtraBars.Navigation.AccordionControl
	End Class
End Namespace