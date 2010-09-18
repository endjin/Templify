namespace Endjin.Templify.Domain.Framework.Container
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;

    #endregion

    public class MefContainer
    {
        protected MefContainer()
        {
            this.Compose();
        }

        private void Compose()
        {
            var catalog = new AggregateCatalog();
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            catalog.Catalogs.Add(new DirectoryCatalog(baseDirectory));
            var container = new CompositionContainer(catalog);

            container.ComposeParts(this);
        }
    }
}