namespace Endjin.Templify.Domain.Contracts.Infrastructure
{
    public interface IConfiguration
    {
        string PackageRepositoryPath { get; set; }

        string GetDirectoryExclusions();

        string GetFileExclusions();

        string GetTokeniseFileExclusions();

        void SaveDirectoryExclusions(string directoryExclusions);

        void SaveFileExclusions(string fileExclusions);

        void SaveTokeniseFileExclusions(string tokeniseFileExclusions);
    }
}