namespace Endjin.Templify.Domain.Contracts.Packager.Tokeniser
{
    using System.Collections.Generic;

    using Endjin.Templify.Domain.Domain.Packages;

    public interface ITemplateTokeniser
    {
        void TokeniseFileContent(string file, List<PackageConfigurationData> tokens);

        void TokeniseDirectoryAndFilePaths(string file, List<PackageConfigurationData> tokens);
    }
}