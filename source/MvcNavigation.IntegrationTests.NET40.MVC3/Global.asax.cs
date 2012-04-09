using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcNavigation.IntegrationTests.NET40.MVC3.Controllers;

namespace MvcNavigation.IntegrationTests.NET40.MVC3
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute("Xml Sitemap", "sitemap.xml", new { controller = "XmlSitemap", action = "Sitemap" });

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			InitialiseNavigation();
			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}

		void InitialiseNavigation()
		{
			var sitemap = new Node<HomeController>(
				c => c.Index(), "Home",
				new Node<HomeController>(c => c.About()));

			NavigationConfiguration.Initialise(sitemap);
		}
	}
}