namespace Endjin.Templify.WizardFramework
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    
    public class StartupForm : BaseWizardForm
	{

		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Button btnBack;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblIntro;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radioCfgSrcDefault;
		private System.Windows.Forms.RadioButton radioCfgSrcOnline;
		private System.Windows.Forms.RadioButton radioCfgSrcCustom;
		private System.Windows.Forms.TextBox txtCfgSrcCustomPath;
		private System.Windows.Forms.Button btnCfgSrcCustomBrowse;
		private System.Windows.Forms.GroupBox groupCfgSrc;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.ComponentModel.IContainer components = null;



		public StartupForm()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StartupForm));
			this.lblIntro = new System.Windows.Forms.Label();
			this.btnNext = new System.Windows.Forms.Button();
			this.btnBack = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupCfgSrc = new System.Windows.Forms.GroupBox();
			this.txtCfgSrcCustomPath = new System.Windows.Forms.TextBox();
			this.btnCfgSrcCustomBrowse = new System.Windows.Forms.Button();
			this.radioCfgSrcDefault = new System.Windows.Forms.RadioButton();
			this.radioCfgSrcOnline = new System.Windows.Forms.RadioButton();
			this.radioCfgSrcCustom = new System.Windows.Forms.RadioButton();
			this.lblTitle = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.groupCfgSrc.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblIntro
			// 
			this.lblIntro.BackColor = System.Drawing.SystemColors.Window;
			this.lblIntro.Location = new System.Drawing.Point(168, 72);
			this.lblIntro.Name = "lblIntro";
			this.lblIntro.Size = new System.Drawing.Size(288, 32);
			this.lblIntro.TabIndex = 0;
			this.lblIntro.Text = "This wizard will guide you through installing the module(s) defined within this i" +
				"nstallation package.";
			// 
			// btnNext
			// 
			this.btnNext.Location = new System.Drawing.Point(304, 320);
			this.btnNext.Name = "btnNext";
			this.btnNext.TabIndex = 0;
			this.btnNext.Text = "&Next";
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// btnBack
			// 
			this.btnBack.Enabled = false;
			this.btnBack.Location = new System.Drawing.Point(216, 320);
			this.btnBack.Name = "btnBack";
			this.btnBack.TabIndex = 1;
			this.btnBack.Text = "&Back";
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(392, 320);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// groupCfgSrc
			// 
			this.groupCfgSrc.BackColor = System.Drawing.SystemColors.Window;
			this.groupCfgSrc.Controls.Add(this.txtCfgSrcCustomPath);
			this.groupCfgSrc.Controls.Add(this.btnCfgSrcCustomBrowse);
			this.groupCfgSrc.Controls.Add(this.radioCfgSrcDefault);
			this.groupCfgSrc.Controls.Add(this.radioCfgSrcOnline);
			this.groupCfgSrc.Controls.Add(this.radioCfgSrcCustom);
			this.groupCfgSrc.Location = new System.Drawing.Point(168, 168);
			this.groupCfgSrc.Name = "groupCfgSrc";
			this.groupCfgSrc.Size = new System.Drawing.Size(296, 120);
			this.groupCfgSrc.TabIndex = 1;
			this.groupCfgSrc.TabStop = false;
			this.groupCfgSrc.Text = "Configuration Management Source";
			// 
			// txtCfgSrcCustomPath
			// 
			this.txtCfgSrcCustomPath.Enabled = false;
			this.txtCfgSrcCustomPath.Location = new System.Drawing.Point(112, 92);
			this.txtCfgSrcCustomPath.Name = "txtCfgSrcCustomPath";
			this.txtCfgSrcCustomPath.Size = new System.Drawing.Size(152, 20);
			this.txtCfgSrcCustomPath.TabIndex = 3;
			this.txtCfgSrcCustomPath.Text = "";
			// 
			// btnCfgSrcCustomBrowse
			// 
			this.btnCfgSrcCustomBrowse.BackColor = System.Drawing.SystemColors.Control;
			this.btnCfgSrcCustomBrowse.Enabled = false;
			this.btnCfgSrcCustomBrowse.Location = new System.Drawing.Point(266, 93);
			this.btnCfgSrcCustomBrowse.Name = "btnCfgSrcCustomBrowse";
			this.btnCfgSrcCustomBrowse.Size = new System.Drawing.Size(22, 16);
			this.btnCfgSrcCustomBrowse.TabIndex = 4;
			this.btnCfgSrcCustomBrowse.Text = "...";
			this.btnCfgSrcCustomBrowse.Click += new System.EventHandler(this.btnCfgSrcCustomBrowse_Click);
			// 
			// radioCfgSrcDefault
			// 
			this.radioCfgSrcDefault.Checked = true;
			this.radioCfgSrcDefault.Location = new System.Drawing.Point(8, 24);
			this.radioCfgSrcDefault.Name = "radioCfgSrcDefault";
			this.radioCfgSrcDefault.Size = new System.Drawing.Size(112, 24);
			this.radioCfgSrcDefault.TabIndex = 0;
			this.radioCfgSrcDefault.TabStop = true;
			this.radioCfgSrcDefault.Text = "Package Defaults";
			// 
			// radioCfgSrcOnline
			// 
			this.radioCfgSrcOnline.Location = new System.Drawing.Point(8, 56);
			this.radioCfgSrcOnline.Name = "radioCfgSrcOnline";
			this.radioCfgSrcOnline.TabIndex = 1;
			this.radioCfgSrcOnline.Text = "Online Source";
			// 
			// radioCfgSrcCustom
			// 
			this.radioCfgSrcCustom.Location = new System.Drawing.Point(8, 89);
			this.radioCfgSrcCustom.Name = "radioCfgSrcCustom";
			this.radioCfgSrcCustom.TabIndex = 2;
			this.radioCfgSrcCustom.Text = "Custom Source";
			this.radioCfgSrcCustom.CheckedChanged += new System.EventHandler(this.radioCfgSrcCustom_CheckedChanged);
			// 
			// lblTitle
			// 
			this.lblTitle.BackColor = System.Drawing.SystemColors.Window;
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTitle.Location = new System.Drawing.Point(168, 16);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(288, 40);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "Welcome to the GPP Installation Wizard";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.Window;
			this.label1.Location = new System.Drawing.Point(168, 120);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(288, 32);
			this.label1.TabIndex = 0;
			this.label1.Text = "First you must select a source of configuration management data to drive the inst" +
				"allation.";
			// 
			// StartupForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(480, 350);
			this.Controls.Add(this.groupCfgSrc);
			this.Controls.Add(this.btnNext);
			this.Controls.Add(this.lblIntro);
			this.Controls.Add(this.btnBack);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "StartupForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Conchango Fusion Installer Wizard";
			this.Load += new System.EventHandler(this.StartupForm_Load);
			this.groupCfgSrc.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		private void btnNext_Click(object sender, System.EventArgs e)
		{
			Utils helper = new Utils();

			// The configuration file selected as this stage is used in 2 ways:
			// 1) Provide a list of environments
			// 2) A path to it is passed to all installations via a defined MSI property

			//foreach ( Control rad in groupCfgSrc.Controls )
			for ( int i=0; i<groupCfgSrc.Controls.Count; i++ )
			{
				if ( groupCfgSrc.Controls[i] is RadioButton )
				{
					RadioButton rad = groupCfgSrc.Controls[i] as RadioButton;
					
					if ( rad.Checked )
					{
						switch ( rad.Name )
						{
							case "radioCfgSrcDefault":
								break;

							case "radioCfgSrcOnline":
								helper.getOnlineConfigFile(ConfigHelper.GetOnlineConfigPath);
								break;

							case "radioCfgSrcCustom":
								WizardState.Instance.UseOtherConfig = true;
								if ( txtCfgSrcCustomPath.Text != string.Empty )
								{
									WizardState.Instance.ConfigFilePath = txtCfgSrcCustomPath.Text;
								}
								break;
						}
					}
				}
			}

			// Finally load the configuration file that drives the pre-installation parameters
			WizardState.Instance.Config = helper.InitFromXmlFile( ConfigHelper.GetParameterConfigFile );

			this.MoveNext();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.MoveExit();
			this.Close();		
		}

		private void radioCfgSrcCustom_CheckedChanged(object sender, System.EventArgs e)
		{
			txtCfgSrcCustomPath.Enabled = radioCfgSrcCustom.Checked;
			btnCfgSrcCustomBrowse.Enabled = radioCfgSrcCustom.Checked;
		}

		private void StartupForm_Load(object sender, System.EventArgs e)
		{
			// Set some title text based on the config file
			this.Text = ConfigHelper.GetWizardTitle;
			lblTitle.Text = ConfigHelper.GetWizardTitleText;
		}

		private void btnCfgSrcCustomBrowse_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.Filter = "Configuration Files (*.xml)|*.xml";
			openFileDialog1.ShowDialog();

			if ( openFileDialog1.FileName != string.Empty )
			{
				txtCfgSrcCustomPath.Text = openFileDialog1.FileName;
			}
		}

	}
}

