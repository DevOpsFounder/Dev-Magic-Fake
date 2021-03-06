﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="http://mohamedradwan.wordpress.com">
//   © 2011 M.Radwan. All rights reserved
// </copyright>
// <summary>
//   The mvc application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using M.Radwan.DevMagicFake.Utilities;

using Microsoft.Practices.Unity;

using TryFakeMVC3.IoC;

#endregion

namespace M.Radwan.TryFakeMVC3
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    /// The mvc application.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        #region Public Methods

        /// <summary>
        /// The register global filters.
        /// </summary>
        /// <param name="filters">
        /// The filters.
        /// </param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        /// <summary>
        /// The register routes.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                );
        }

        #endregion

        #region Methods

        /// <summary>
        /// The application_ end.
        /// </summary>
        protected void Application_End()
        {
            //Utilitie.BinarySerialize();
        }

        /// <summary>
        /// The application_ start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            IUnityContainer container = this.GetUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            //Utilitie.BinaryDeserialize();
        }

        /// <summary>
        /// The get unity container.
        /// </summary>
        /// <returns>
        /// </returns>
        private IUnityContainer GetUnityContainer()
        {
            // Create UnityContainer          
            IUnityContainer container = new UnityContainer().RegisterType<IControllerActivator, CustomControllerActivator>();

            // .RegisterType<VendorForm, VendorForm>()
            // .RegisterType<IController, VendorController>("vendor");
            return container;
        }

        #endregion
    }
}