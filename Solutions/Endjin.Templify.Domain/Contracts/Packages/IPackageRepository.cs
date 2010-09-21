namespace Endjin.Templify.Domain.Contracts.Packages
{
    #region Using Directives

    using System;
    using System.Linq;

    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    public interface IPackageRepository
    {
        IQueryable<Package> FindAll();
        
        Package FindOne(Guid id);
        
        void Remove(Package package);
    }
}