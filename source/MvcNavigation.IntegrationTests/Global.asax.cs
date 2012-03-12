// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcNavigation.IntegrationTests.Controllers;

namespace MvcNavigation.IntegrationTests
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			RegisterRoutes(RouteTable.Routes);

			ConfigureNavigation();
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute("Home", "", new { controller = "Home", action = "Home" });
			routes.MapRoute("About", "about", new { controller = "Home", action = "About" });
			routes.MapRoute("Contact", "contact", new { controller = "Home", action = "Contact" });

			routes.MapRoute("Navigation", "navigation", new { controller = "Home", action = "Navigation" });
		}

		void ConfigureNavigation()
		{
			var rootNode = new Node<HomeController>(
				c => c.Index(),
				new Node<HomeController>(c => c.About()),
				new Node<HomeController>(c => c.Contact())
				);

			NavigationConfiguration.Initialise(rootNode);
		}
	}
}