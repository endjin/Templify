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

        Package FindOne(string name);
        
        void Remove(Package package);
    }
}