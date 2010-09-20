namespace Endjin.Templify.Domain.Contracts.Infrastructure
{
    public interface IConfiguration
    {
        string GetFileExclusions();

        string GetDirectoryExclusions();
    }
}