
namespace CustomItemTest {
    partial class DesignerForm1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.dashboardBarAndDockingController1 = new DevExpress.DashboardWin.Native.DashboardBarAndDockingController(this.components);
            this.dashboardDesigner = new DevExpress.DashboardWin.DashboardDesigner();
            this.dashboardsAccordion = new DevExpress.XtraBars.Navigation.AccordionControl();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardBarAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardDesigner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardsAccordion)).BeginInit();
            this.SuspendLayout();
            // 
            // dashboardBarAndDockingController1
            // 
            this.dashboardBarAndDockingController1.PropertiesDocking.ViewStyle = DevExpress.XtraBars.Docking2010.Views.DockingViewStyle.Classic;
            // 
            // dashboardDesigner
            // 
            this.dashboardDesigner.AsyncMode = true;
            this.dashboardDesigner.BarAndDockingController = this.dashboardBarAndDockingController1;
            this.dashboardDesigner.DataSourceWizard.ShowConnectionsFromAppConfig = true;
            this.dashboardDesigner.DataSourceWizard.SqlWizardSettings.DatabaseCredentialsSavingBehavior = DevExpress.DataAccess.Wizard.SensitiveInfoSavingBehavior.Prompt;
            this.dashboardDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashboardDesigner.Location = new System.Drawing.Point(227, 0);
            this.dashboardDesigner.Name = "dashboardDesigner";
            this.dashboardDesigner.Size = new System.Drawing.Size(963, 595);
            this.dashboardDesigner.TabIndex = 0;
            this.dashboardDesigner.UseNeutralFilterMode = true;
            // 
            // dashboardsAccordion
            // 
            this.dashboardsAccordion.AllowItemSelection = true;
            this.dashboardsAccordion.Dock = System.Windows.Forms.DockStyle.Left;
            this.dashboardsAccordion.Location = new System.Drawing.Point(0, 0);
            this.dashboardsAccordion.Name = "dashboardsAccordion";
            this.dashboardsAccordion.Size = new System.Drawing.Size(227, 595);
            this.dashboardsAccordion.TabIndex = 1;
            this.dashboardsAccordion.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            this.dashboardsAccordion.SelectedElementChanged += new DevExpress.XtraBars.Navigation.SelectedElementChangedEventHandler(this.dashboardsAccordion_SelectedElementChanged);
            // 
            // DesignerForm1
            // 
            this.AllowFormGlass = DevExpress.Utils.DefaultBoolean.False;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 595);
            this.Controls.Add(this.dashboardDesigner);
            this.Controls.Add(this.dashboardsAccordion);
            this.Name = "DesignerForm1";
            this.Text = "Dashboard Designer";
            ((System.ComponentModel.ISupportInitialize)(this.dashboardBarAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardDesigner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboardsAccordion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.DashboardWin.DashboardDesigner dashboardDesigner;
        private DevExpress.DashboardWin.Native.DashboardBarAndDockingController dashboardBarAndDockingController1;
        private DevExpress.XtraBars.Navigation.AccordionControl dashboardsAccordion;
    }
}