namespace Endjin.Templify.Domain.Domain.Packages
{
    using System.ComponentModel;

    public enum ProgressStage
    {
        [Description("Build Package File Manifest")]
        BuildManifest,

        [Description("Build Package")]
        BuildPackage,

        [Description("Clone Package")]
        ClonePackage,

        [Description("Tokenise Package Contents")]
        TokenisePackageContents,

        [Description("Tokenise Package Structure")]
        TokenisePackageStructure,

        [Description("Build Package Archive")]
        BuildArchive,

        [Description("Save Package Archive")]
        SaveArchive,

        [Description("Clean Up Temporary Files")]
        CleanUp,

        [Description("Extract Files From Package")]
        ExtractFilesFromPackage,
    }
}