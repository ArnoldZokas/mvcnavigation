// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcNavigation.Examples.Controllers;

namespace MvcNavigation.Examples
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
		}

		void ConfigureNavigation()
		{
			var rootNode = new Node<HomeController>(
				c => c.Home(),
				new Node<HomeController>(c => c.About()),
				new Node<HomeController>(c => c.Contact())
				);

			SitemapConfiguration.Initialise(rootNode);
		}
	}
}