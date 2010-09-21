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
        private readonly IArtefactProcessor artefactProcessor;
        private readonly IPackageFactory packageFactory;

        [ImportingConstructor]
        public PackageRepository(IArtefactProcessor artefactProcessor, IPackageFactory packageFactory)
        {
            this.artefactProcessor = artefactProcessor;
            this.packageFactory = packageFactory;
        }

        public IQueryable<Package> FindAll()
        {
            var files = this.artefactProcessor.RetrieveFiles(FilePaths.PackageRepository, FileTypes.PackageWildcard);

            return files.Select(this.packageFactory.Get).Where(p => !string.IsNullOrEmpty(p.Manifest.Name)).AsQueryable();
        }

        public Package FindOne(Guid id)
        {
            return this.FindAll().Where(p => p.Manifest.Id == id).FirstOrDefault();
        }

        public void Remove(Package package)
        {
            this.artefactProcessor.RemoveFile(package.Manifest.Path);
        }
    }
}