namespace Endjin.Templify.Domain.Infrastructure
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.Configuration;

    using Endjin.Templify.Domain.Contracts.Infrastructure;

    #endregion

    [Export(typeof(IConfiguration))]
    public class Configuration : IConfiguration
    {
        public string GetFileExclusions()
        {
            return this.GetConfigSetting("FileExclusions");
        }
        
        public string GetDirectoryExclusions()
        {
            return this.GetConfigSetting("DirectoryExclusions");
        }

        public void SaveDirectoryExclusions(string directoryExclusions)
        {
            this.SaveConfigSetting("DirectoryExclusions", directoryExclusions);
        }

        public void SaveFileExclusions(string fileExclusions)
        {
            this.SaveConfigSetting("FileExclusions", fileExclusions);
        }

        private string GetConfigSetting(string settingName)
        {
            return ConfigurationManager.AppSettings[settingName] ?? string.Empty;
        }

        private void SaveConfigSetting(string settingName, string value)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[settingName].Value = value;
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}