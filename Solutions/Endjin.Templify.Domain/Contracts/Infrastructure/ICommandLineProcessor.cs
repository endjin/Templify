namespace Endjin.Templify.Domain.Contracts.Infrastructure
{
    using Endjin.Templify.Domain.Infrastructure;

    public interface ICommandLineProcessor
    {
        CommandOptions Process(string[] args);
    }
}