namespace Endjin.Templify.Domain.Contracts.Packager.Tokeniser
{
    public interface IReservedTokenResolver
    {
        string Resolve(string item, string path);
    }
}