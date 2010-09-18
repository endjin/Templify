namespace Endjin.Templify.Client.Core
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Caliburn.Micro;

    #endregion

    public class MefBootstrapper<T> : Bootstrapper<T>
    {
        private CompositionContainer container;
       
        protected override void BuildUp(object instance)
        {
            this.container.SatisfyImportsOnce(instance);
        }

        protected override void Configure()
        {
            this.container = new CompositionContainer(
                                new AggregateCatalog(
                                    AssemblySource.Instance
                                                  .Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()));
            
            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(this.container);

            this.container.Compose(batch);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return this.container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = this.container.GetExportedValues<object>(contract);

            if (exports.Count() > 0)
            {
                return exports.First();
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] { Assembly.GetExecutingAssembly(), Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Endjin.Templify.Domain.dll")) };
        }
    }
}