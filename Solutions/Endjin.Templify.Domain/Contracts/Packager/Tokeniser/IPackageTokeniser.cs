namespace Endjin.Templify.Domain.Contracts.Packager.Tokeniser
{
    #region Using Directives

    using Endjin.Templify.Domain.Domain.Packages;

    #endregion;

    public interface IPackageTokeniser
    {
        Package Tokenise(Package package, string token);
    }
}