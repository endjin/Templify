namespace Endjin.Templify.Domain.Contracts.Packager.Processors
{
    public interface ICloneFileProcessor
    {
        void Process(string sourcePath, string destinationPath);
    }
}