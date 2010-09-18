namespace Endjin.Templify.Domain.Infrastructure.Repositories
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packages;
    using Endjin.Templify.Domain.Domain.Factories;
    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    [Export(typeof(IPackageRepository))]
    public class PackageRepository : IPackageRepository
    {
        private readonly string repositoryPath;
        private readonly IArtefactProcessor fileSystemArtecactProcessor;

        [ImportingConstructor]
        public PackageRepository(IArtefactProcessor fileSystemArtecactProcessor)
        {
            this.fileSystemArtecactProcessor = fileSystemArtecactProcessor;

            this.repositoryPath = FilePaths.PackageRepository;
        }

        public IQueryable<Package> FindAll()
        {
            var files = this.fileSystemArtecactProcessor.RetrieveFiles(this.repositoryPath, "*.pkg");

            return files.Select(PackageFactory.Get).Where(p => !string.IsNullOrEmpty(p.Manifest.Name)).AsQueryable();
        }

        public Package FindOne(Guid id)
        {
            return this.FindAll().Where(p => p.Manifest.Id == id).FirstOrDefault();
        }
    }
}