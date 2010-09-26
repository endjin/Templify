namespace Endjin.Templify.Domain.Domain.Packager.Tokeniser
{
    #region Using Directives

    using System.ComponentModel.Composition;

    using Endjin.Templify.Domain.Contracts.Packager.Tokeniser;

    #endregion

    [Export(typeof(IReservedTokenResolver))]
    public class ReservedTokenResolver : IReservedTokenResolver
    {
        public string Resolve(string item, string path)
        {
            // Resolve any reserved tokens
            return item.Replace("$(InstallRoot)", path);
        }
    }
}