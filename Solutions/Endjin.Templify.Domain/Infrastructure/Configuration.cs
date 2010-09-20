using System.ComponentModel.Composition;
using System.Configuration;
using Endjin.Templify.Domain.Contracts.Infrastructure;

namespace Endjin.Templify.Domain.Infrastructure
{
    [Export(typeof(IConfiguration))]
    public class Configuration : IConfiguration
    {
        public string GetFileExclusions()
        {
            return GetConfigSetting("FileExclusions");
        }
        
        public string GetDirectoryExclusions()
        {
            return GetConfigSetting("DirectoryExclusions");
        }

        private string GetConfigSetting(string settingName)
        {
            return ConfigurationManager.AppSettings[settingName] ?? string.Empty;
        }
    }
}