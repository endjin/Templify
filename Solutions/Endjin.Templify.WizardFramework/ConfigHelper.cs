using System;
using System.Configuration;

namespace Conchango.Fusion.InstallerWizard
{
	/// <summary>
	/// Summary description for ConfigHelper.
	/// </summary>
	public class ConfigHelper
	{

		private static string _wizardTitle = ConfigurationSettings.AppSettings["WizardTitle"];
		private static string _wizardTitleText = ConfigurationSettings.AppSettings["WizardTitleText"];
		private static string _parameterConfigFile = ConfigurationSettings.AppSettings["ParameterConfigFile"];
		private static string _onlineConfigPath = ConfigurationSettings.AppSettings["CfgSrcOnlinePath"];
		private static string _localConfigPath = ConfigurationSettings.AppSettings["CfgSrcLocalPath"];


		private ConfigHelper()
		{

		}

		public static string GetWizardTitle
		{
			get { return _wizardTitle; }
		}

		public static string GetWizardTitleText
		{
			get { return _wizardTitleText; }
		}

		public static string GetParameterConfigFile
		{
			get { return _parameterConfigFile; }
		}

		public static string GetOnlineConfigPath
		{
			get { return _onlineConfigPath; }
		}

		public static string GetLocalConfigPath
		{
			get { return _localConfigPath; }
		}
	}
}
