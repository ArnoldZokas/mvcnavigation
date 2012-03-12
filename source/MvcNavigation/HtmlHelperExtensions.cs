// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Web.Mvc;
using System.Web.Mvc.Html;
using MvcNavigation.Configuration.Advanced;

namespace MvcNavigation
{
	public static class HtmlHelperExtensions
	{
		public static MvcHtmlString Menu(this HtmlHelper html)
		{
			// TODO: test null sitemap

			var rootNode = NavigationConfiguration.Sitemap;
			return RendererConfiguration.MenuRenderer(html, rootNode);
		}

		public static MvcHtmlString ActionLink(this HtmlHelper helper, INode linkTarget)
		{
			// TODO: test null linkTarget

			return helper.ActionLink(linkTarget.Text, linkTarget.ActionName, linkTarget.ControllerName);
		}
	}
}