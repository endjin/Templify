namespace Endjin.Templify.WizardFramework
{
    using System;
    using System.Collections.Generic;

	using Endjin.Templify.Domain.Domain.Packages;

    public class PackageConfigurationDataWizardFormCommand : FormWizardCommand 
	{
        private List<PackageConfigurationData> settings;

        public PackageConfigurationDataWizardFormCommand(List<PackageConfigurationData> Settings)
        {
            this.settings = Settings;
        }

		/// <summary>
		/// Sets the form to use as the command form
		/// </summary>
		public override void InitialiseCommand() 
		{
            this.CommandForm = new PackageConfigurationDataWizardForm(this.settings);
		}

		/// <summary>
		/// Implements any cleanup required
		/// </summary>
		public override void CleanupCommand() 
		{

		}
	}
}
