namespace Endjin.Templify.Domain.Contracts.Infrastructure
{
    public interface IConfiguration
    {
        string GetFileExclusions();

        string GetDirectoryExclusions();

        void SaveDirectoryExclusions(string directoryExclusions);

        void SaveFileExclusions(string fileExclusions);
    }
}