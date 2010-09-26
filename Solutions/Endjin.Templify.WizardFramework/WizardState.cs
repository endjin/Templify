namespace Endjin.Templify.WizardFramework
{
    using System;
    using System.Collections.Generic;

    using Endjin.Templify.Domain.Domain.Packages;

	/// <summary>
	/// Summary description for WizardState.
	/// </summary>
	public class WizardState
	{
        public List<PackageConfigurationData> Settings { get; set; }


		// Public instance member 
		public static WizardState Instance = new WizardState();


		private WizardState()
		{
		}
	}
}
