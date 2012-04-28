namespace Endjin.Templify.Domain.Infrastructure
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;
    using System.Configuration;
    using System.IO;

    using Endjin.Templify.Domain.Contracts.Infrastructure;

    #endregion

    [Export(typeof(IConfiguration))]
    public class Configuration : IConfiguration
    {
        public Configuration()
        {
            this.PackageRepositoryPath = FilePaths.PackageRepository;
        }

        public string PackageRepositoryPath { get; set; }

        public string GetDirectoryExclusions()
        {
            return this.GetConfigSetting("DirectoryExclusions");
        }

        public string GetFileExclusions()
        {
            return this.GetConfigSetting("FileExclusions");
        }

        public string GetTokeniseFileExclusions()
        {
            return this.GetConfigSetting("TokeniseFileExclusions");
        }

        public void SaveDirectoryExclusions(string directoryExclusions)
        {
            this.SaveConfigSetting("DirectoryExclusions", directoryExclusions);
        }

        public void SaveFileExclusions(string fileExclusions)
        {
            this.SaveConfigSetting("FileExclusions", fileExclusions);
        }

        public void SaveTokeniseFileExclusions(string tokeniseFileExclusions)
        {
            this.SaveConfigSetting("TokeniseFileExclusions", tokeniseFileExclusions);
        }

        private System.Configuration.Configuration GetConfiguration()
        {
            var exeConfig = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = Path.Combine(FilePaths.ConfigurationDirectory, "Templify.config")
                };

            return ConfigurationManager.OpenMappedExeConfiguration(exeConfig, ConfigurationUserLevel.None);
        }

        private string GetConfigSetting(string settingName)
        {
            var configuration = this.GetConfiguration();
            var setting = string.Empty;

            if (configuration.AppSettings.Settings[settingName] != null)
            {
                setting = configuration.AppSettings.Settings[settingName].Value ?? string.Empty;
            }

            return setting;
        }

        private void SaveConfigSetting(string settingName, string value)
        {
            var configuration = this.GetConfiguration();
            
            configuration.AppSettings.Settings[settingName].Value = value;
            configuration.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}