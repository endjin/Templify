namespace Endjin.Templify.Domain.Contracts.Packager.Processors
{
    public interface IRenameFileProcessor
    {
        void Process(string oldName, string newName);
    }
}