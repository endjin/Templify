namespace Endjin.Templify.Domain.Contracts.Packager.Tokeniser
{
    #region Using Directives

    using System.Collections.Generic;

    using Endjin.Templify.Domain.Domain.Packages;

    #endregion;

    public interface IPackageTokeniser
    {
        Package Tokenise(Package package, Dictionary<string, string> tokens);
    }
}