namespace OBD_Simple
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox_Search_Item = new System.Windows.Forms.TextBox();
            this.button_Search = new System.Windows.Forms.Button();
            this.comboBox_Search_Results = new System.Windows.Forms.ComboBox();
            this.label_Info_Personnel = new System.Windows.Forms.Label();
            this.inputPanel = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.label_Info_Car_Registrations = new System.Windows.Forms.Label();
            this.label_Search_Info = new System.Windows.Forms.Label();
            this.lBox_Personnel = new System.Windows.Forms.ListBox();
            this.lBox_Vehicles = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // textBox_Search_Item
            // 
            resources.ApplyResources(this.textBox_Search_Item, "textBox_Search_Item");
            this.textBox_Search_Item.Name = "textBox_Search_Item";
            this.textBox_Search_Item.GotFocus += new System.EventHandler(this.textBox_Search_Item_GotFocus);
            this.textBox_Search_Item.LostFocus += new System.EventHandler(this.textBox_Search_Item_LostFocus);
            // 
            // button_Search
            // 
            resources.ApplyResources(this.button_Search, "button_Search");
            this.button_Search.Name = "button_Search";
            this.button_Search.Click += new System.EventHandler(this.button_Search_Click);
            // 
            // comboBox_Search_Results
            // 
            resources.ApplyResources(this.comboBox_Search_Results, "comboBox_Search_Results");
            this.comboBox_Search_Results.Name = "comboBox_Search_Results";
            this.comboBox_Search_Results.SelectedIndexChanged += new System.EventHandler(this.comboBox_Search_Results_SelectedIndexChanged);
            // 
            // label_Info_Personnel
            // 
            resources.ApplyResources(this.label_Info_Personnel, "label_Info_Personnel");
            this.label_Info_Personnel.Name = "label_Info_Personnel";
            // 
            // label_Info_Car_Registrations
            // 
            resources.ApplyResources(this.label_Info_Car_Registrations, "label_Info_Car_Registrations");
            this.label_Info_Car_Registrations.Name = "label_Info_Car_Registrations";
            // 
            // label_Search_Info
            // 
            resources.ApplyResources(this.label_Search_Info, "label_Search_Info");
            this.label_Search_Info.Name = "label_Search_Info";
            // 
            // lBox_Personnel
            // 
            resources.ApplyResources(this.lBox_Personnel, "lBox_Personnel");
            this.lBox_Personnel.Name = "lBox_Personnel";
            // 
            // lBox_Vehicles
            // 
            resources.ApplyResources(this.lBox_Vehicles, "lBox_Vehicles");
            this.lBox_Vehicles.Name = "lBox_Vehicles";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.label_Search_Info);
            this.Controls.Add(this.label_Info_Car_Registrations);
            this.Controls.Add(this.label_Info_Personnel);
            this.Controls.Add(this.comboBox_Search_Results);
            this.Controls.Add(this.button_Search);
            this.Controls.Add(this.textBox_Search_Item);
            this.Controls.Add(this.lBox_Personnel);
            this.Controls.Add(this.lBox_Vehicles);
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Search_Item;
        private System.Windows.Forms.Button button_Search;
        private System.Windows.Forms.ComboBox comboBox_Search_Results;
        private System.Windows.Forms.Label label_Info_Personnel;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel;
        private System.Windows.Forms.Label label_Info_Car_Registrations;
        private System.Windows.Forms.Label label_Search_Info;
        private System.Windows.Forms.ListBox lBox_Personnel;
        private System.Windows.Forms.ListBox lBox_Vehicles;
    }
}

