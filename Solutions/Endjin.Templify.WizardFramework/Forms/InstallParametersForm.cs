namespace Endjin.Templify.WizardFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Diagnostics;
    //using System.DirectoryServices;
    //using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    using Endjin.Templify.Domain.Domain.Packages;

    public class PackageConfigurationDataWizardForm : BaseWizardForm
    {
        private const int MAX_SETTINGS_PER_PAGE = 5;

        private int currentPage = 0;
        //private InstallerBootstrapConfigPage[] pages;
        //private IisHostHeaderConfigCollection webInfoForComboBox;


        #region UI cordinates
        // These values are used to derive the positions of the
        // dynamically generated controls
        private int firstLabelX = 8;
        private int firstLabelY = 31;
        private int labelWidth = 152;
        private int labelHeight = 20;

        private int firstControlX = 167;
        private int firstControlY = 29;
        private int controlWidth = 216;
        private int controlHeight = 20;

        #endregion


        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupParameters;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.ComponentModel.IContainer components = null;

        public PackageConfigurationDataWizardForm(List<PackageConfigurationData> settings)
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            //pages = (InstallerBootstrapConfigPage[])WizardState.Instance.RequiredParameters.ToArray(typeof(InstallerBootstrapConfigPage) );
            this.Settings = settings;
        }

        public List<PackageConfigurationData> Settings { get; set; }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PackageConfigurationDataWizardForm));
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupParameters = new System.Windows.Forms.GroupBox();
            this.lblText = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.Window;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(24, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(384, 40);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Pre-Installation Parameters";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(304, 320);
            this.btnNext.Name = "btnNext";
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "&Next";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(216, 320);
            this.btnBack.Name = "btnBack";
            this.btnBack.TabIndex = 12;
            this.btnBack.Text = "&Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(392, 320);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupParameters
            // 
            this.groupParameters.BackColor = System.Drawing.SystemColors.Window;
            this.groupParameters.Location = new System.Drawing.Point(32, 112);
            this.groupParameters.Name = "groupParameters";
            this.groupParameters.Size = new System.Drawing.Size(416, 184);
            this.groupParameters.TabIndex = 0;
            this.groupParameters.TabStop = false;
            this.groupParameters.Text = "<parameters>";
            // 
            // lblText
            // 
            this.lblText.BackColor = System.Drawing.SystemColors.Window;
            this.lblText.Location = new System.Drawing.Point(40, 72);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(408, 32);
            this.lblText.TabIndex = 16;
            this.lblText.Text = "Enter the correct settings for the following parameters required by this installa" +
                "tion package.";
            // 
            // InstallParametersForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(478, 348);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.groupParameters);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InstallParametersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.InstallParametersForm_Load);
            this.ResumeLayout(false);

        }
        #endregion

        private void btnBack_Click(object sender, System.EventArgs e)
        {
            this.MovePrevious();
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            // Save the parameter info from the current screen
            saveCurrentParameters();

            currentPage++;

            if (currentPage <= Settings.Count / MAX_SETTINGS_PER_PAGE)
            {
                // Display theh UI to obtain values for the required parameters
                renderParameterPage(currentPage);
            }
            else
            {
                // Generate the set of Windows Installer properties that map to the
                // parameters that the user has just supplied
                //buildMsiPropertyList();

                // Proceed to next stage in the Wizard
                this.MoveNext();
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.MoveExit();
            this.Close();
        }

        private void InstallParametersForm_Load(object sender, System.EventArgs e)
        {
            // Set some title text based on the config file
            this.Text = "*** TO BE REPLACED ***";

            renderParameterPage(currentPage);
        }





        private void renderParameterPage(int pageNumber)
        {
            clearControls();

            //var settingsToRender = from s in this.settings
            //                       select s;
            var settingsToRender = this.Settings.Skip((pageNumber - 1) * MAX_SETTINGS_PER_PAGE).Take(MAX_SETTINGS_PER_PAGE).ToList();

            // set the variables
            int numControlsReqd = settingsToRender.Count();

            // setup the groupbox title
            //groupParameters.Text = string.Format("{0} of {1} - {2}", currentPage + 1, pages.Length, this.pages[currentPage].Title);
            groupParameters.Text = "Page " + pageNumber;

            #region setup the label controls
            for (int i = 1; i <= numControlsReqd; i++)
            {
                // create a new label
                Label lbl = new Label();

                // position the label
                lbl.Left = firstLabelX;
                lbl.Top = firstLabelY * i;
                lbl.Width = labelWidth;
                lbl.Height = labelHeight;

                // setup the control
                lbl.Text = settingsToRender[i].Description + ":";

                // Set the tooltip using the supplied description
                toolTip1.SetToolTip(lbl, settingsToRender[i].Description);

                lbl.Visible = true;

                groupParameters.Controls.Add(lbl);

            }
            #endregion

            #region setup the input controls
            for (int i = 1; i <= numControlsReqd; i++)
            {

                if (settingsToRender[i].Kind == PackageConfigurationDataKind.choice)
                {
                    ComboBox inputControl = new ComboBox();

                    // TODO: populate combo box
                    // inputControl.DataSource = settingsToRender[i].Default.Split('|');

                    // postition the control
                    inputControl.Left = firstControlX;
                    inputControl.Top = firstControlY * i;
                    inputControl.Width = controlWidth;
                    inputControl.Height = controlHeight;

                    inputControl.Visible = true;

                    groupParameters.Controls.Add(inputControl);
                }
                else
                {
                    TextBox inputControl = new TextBox();

                    // Set the tooltip using the supplied description
                    //inputControl.Text = this.pages[currentPage].Parameters[i - 1].DefaultValue;
                    toolTip1.SetToolTip(inputControl, settingsToRender[i].Description);

                    if (settingsToRender[i].Kind == PackageConfigurationDataKind.password)
                    {
                        inputControl.PasswordChar = '*';
                    }

                    // position the control
                    inputControl.Left = firstControlX;
                    inputControl.Top = firstControlY * i;
                    inputControl.Width = controlWidth;
                    inputControl.Height = controlHeight;

                    inputControl.Visible = true;

                    groupParameters.Controls.Add(inputControl);
                }
            }
            #endregion
        }

        private void saveCurrentParameters()
        {
            var settingsToRender = this.Settings.Skip((currentPage - 1) * MAX_SETTINGS_PER_PAGE).Take(MAX_SETTINGS_PER_PAGE).ToList();

            for (int i = 0; i < settingsToRender.Count(); i++)
            {
                string paramName = settingsToRender[i].Token;
                string paramValue = GetControlValue(i);

                if (paramValue != null)
                {
                    settingsToRender[i].Value = paramValue;

                    //if (this.pages[currentPage].Parameters[i].ParamType == InstallerBootstrapConfigPageParameterParamType.path)
                    //{
                    //    // Ensure that path parameters have a trailing
                    //    // '\' - as this is required by MSI
                    //    if (!paramValue.EndsWith(@"\"))
                    //    {
                    //        paramValue = string.Concat(paramValue, @"\");
                    //    }
                    //}

                    // Get the parameter's unique ID, we use this to differentiate between parameters
                    // of the same name
                    //string paramId = this.pages[currentPage].Parameters[i].id + ".";

                    //if (this.pages[currentPage].Parameters[i].ParamType == InstallerBootstrapConfigPageParameterParamType.website)
                    //{
                    //    // If dealing with a web site parameter, the this actually requires three
                    //    // bits of information - which (for the moment?) has to be handled using this hack.

                    //    // Such parameters must be defined in the parameter WizardState.Instance.Config as 3 comma seperated
                    //    // strings which we split below and return as seperate parameters to the bootstrapper
                    //    string[] splitParameterNames = paramName.Split(",".ToCharArray(), 3);
                    //    WizardState.Instance.ParameterValues.Add(string.Concat(paramId, splitParameterNames[0]), paramValue);
                    //    WizardState.Instance.ParameterValues.Add(string.Concat(paramId, splitParameterNames[1]), webInfoForComboBox[paramValue].Address);
                    //    WizardState.Instance.ParameterValues.Add(string.Concat(paramId, splitParameterNames[2]), webInfoForComboBox[paramValue].Port);
                    //}
                    //else
                    //{
                        // otherwise just assign the value to the parameter
                        //WizardState.Instance.Settings.Add(string.Concat(paramId, paramName), paramValue);
                    //}
                }
            }
        }

        private void clearControls()
        {
            // Clear all the existing intput controls
            if (groupParameters.HasChildren)
            {
                //foreach ( Control control in grpParamTitle.Controls )
                //{
                //	control.Dispose();
                //}
                groupParameters.Controls.Clear();
            }

        }

        private string GetControlValue(int i)
        {
            // skip the labels
            int firstInputControl = (groupParameters.Controls.Count / 2);

            int requiredInputControl = firstInputControl + i;

            return groupParameters.Controls[requiredInputControl].Text;
        }

    }
}