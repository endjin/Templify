namespace Endjin.Templify.Domain.Domain.Packages
{
    using System.ComponentModel;

    public enum ProgressStage
    {
        [Description("Build Package File Manifest")]
        BuildManifest,

        [Description("Build Package")]
        BuildPackage,

        [Description("Clean Up Temporary Files")]
        CleanUp,

        [Description("Clone Package")]
        ClonePackage,

        [Description("Creating Package Archive")]
        CreatingArchive,

        [Description("Extract Files From Package")]
        ExtractFilesFromPackage,

        [Description("Tokenise Package Contents")]
        TokenisePackageContents,

        [Description("Tokenise Package Structure")]
        TokenisePackageStructure,
    }
}