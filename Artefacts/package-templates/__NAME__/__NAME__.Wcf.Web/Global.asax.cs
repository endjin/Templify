using System;
using Castle.Windsor;
using System.Web;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;
using SharpArch.Data.NHibernate;
using SharpArch.Wcf.NHibernate;
using Northwind.Data.NHibernateMaps;
using Northwind.Wcf.Web.CastleWindsor;
using System.Reflection;

namespace Northwind.Wcf.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start() {
            IWindsorContainer container = InitializeServiceLocator();
        }

        /// <summary>
        /// Instantiate the container and add all Controllers that derive from 
        /// WindsorController to the container.  Also associate the Controller 
        /// with the WindsorContainer ControllerFactory.
        /// </summary>
        protected virtual IWindsorContainer InitializeServiceLocator() {
            IWindsorContainer container = new WindsorContainer();
            ComponentRegistrar.AddComponentsTo(container);
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
            return container;
        }

        public override void Init() {
            base.Init();

            // The WebSessionStorage must be created during the Init() to tie in HttpApplication events
            wcfSessionStorage = new WcfSessionStorage();
        }

        /// <summary>
        /// Due to issues on IIS7, the NHibernate initialization cannot reside in Init() but
        /// must only be called once.  Consequently, we invoke a thread-safe singleton class to 
        /// ensure it's only initialized once.
        /// </summary>
        protected void Application_BeginRequest(object sender, EventArgs e) {
            NHibernateInitializer.Instance().InitializeNHibernateOnce(
                () => InitializeNHibernateSession());
        }

        /// <summary>
        /// If you need to communicate to multiple databases, you'd add a line to this method to
        /// initialize the other database as well.
        /// </summary>
        private void InitializeNHibernateSession() {
            NHibernateSession.Init(
                wcfSessionStorage,
                new string[] { Server.MapPath("~/bin/Northwind.Data.dll") },
                new AutoPersistenceModelGenerator().Generate(),
                Server.MapPath("~/NHibernate.config"));
        }

        protected void Application_Error(object sender, EventArgs e) {
            // Useful for debugging
            Exception ex = Server.GetLastError();
            ReflectionTypeLoadException reflectionTypeLoadException = ex as ReflectionTypeLoadException;
        }

        private WcfSessionStorage wcfSessionStorage;
    }
}