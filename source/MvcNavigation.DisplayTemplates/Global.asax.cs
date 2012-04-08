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

			AreaRegistration.RegisterAllAreas();

			ConfigureNavigation();
			ConfigureCmsNavigation();
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute("Home", "", new { controller = "Home", action = "Home" });

			routes.MapRoute("Contact", "contact", new { controller = "Home", action = "Contact" });
			routes.MapRoute("Contact/Page 1", "contact/page-1", new { controller = "Home", action = "ContactPage1" });
			routes.MapRoute("Contact/Page 2", "contact/page-2", new { controller = "Home", action = "ContactPage2" });

			routes.MapRoute("CMS/Dashboard", "cms/dashboard", new { controller = "Cms", action = "Dashboard" });
			routes.MapRoute("CMS/Pages", "cms/pages", new { controller = "Cms", action = "Pages" });
			routes.MapRoute("CMS/Users", "cms/users", new { controller = "Cms", action = "Users" });

			routes.MapRoute("Navigation", "navigation", new { controller = "Home", action = "Navigation" });
		}

		static void ConfigureNavigation()
		{
			var rootNode = new Node<HomeController>(
				c => c.Index(),
				new ShopNode<ProductController, ShopAreaRegistration>(c => c.Index()),
				new Node<HomeController, AboutAreaRegistration>(
					c => c.About(),
					new Node<HomeController, AboutAreaRegistration>(c => c.History()),
					new Node<HomeController, AboutAreaRegistration>(c => c.MoreHistory(), "More History")),
				new Node<HomeController>(
					c => c.Contact(), "Contact Us",
					new Node<HomeController>(c => c.ContactPage1(), "Page 1"),
					new Node<HomeController>(c => c.ContactPage2(), "Page 2"))
				);

			NavigationConfiguration.Initialise(rootNode);
		}

		static void ConfigureCmsNavigation()
		{
			var rootNode = new Node<HomeController>(
				c => c.Index(),
				new Node<CmsController>(c => c.Dashboard()),
				new Node<CmsController>(c => c.Pages()),
				new Node<CmsController>(c => c.Users())
				);

			NavigationConfiguration.Initialise("Cms", rootNode);
		}
	}

	public class AboutAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get { return "About"; }
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute("About", "about", new { controller = "Home", action = "About" });
			context.MapRoute("About/History", "about/history", new { controller = "Home", action = "History" });
			context.MapRoute("About/MoreHistory", "about/more-history", new { controller = "Home", action = "MoreHistory" });
		}
	}

	public class ShopAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get { return "Shop"; }
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute("Shop", "shop", new { controller = "Product", action = "Shop" });
			context.MapRoute("Shop/Category", "shop/category/{id}", new { controller = "Product", action = "Category" });
		}
	}
}