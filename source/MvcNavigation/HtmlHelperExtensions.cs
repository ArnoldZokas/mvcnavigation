// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MvcNavigation.Configuration.Advanced;

namespace MvcNavigation
{
	public static class HtmlHelperExtensions
	{
		public static MvcHtmlString Menu(this HtmlHelper html)
		{
			if (NavigationConfiguration.Sitemap == null)
				throw new InvalidOperationException("MvcNavigation is not initialised.");

			var rootNode = NavigationConfiguration.Sitemap;
			return RendererConfiguration.MenuRenderer(html, rootNode);
		}

		public static MvcHtmlString ActionLink(this HtmlHelper html, INode linkTarget)
		{
			if (html == null)
				throw new ArgumentNullException("html");

			if (linkTarget == null)
				throw new ArgumentNullException("linkTarget");

			// TODO: make class configurable
			object htmlAttributes = null;
			if (IsCurrentNode(html, linkTarget))
				htmlAttributes = new { @class = "current" };

			return html.ActionLink(linkTarget.Text, linkTarget.ActionName, linkTarget.ControllerName, null, htmlAttributes);
		}

		public static bool IsCurrentNode(this HtmlHelper html, INode node)
		{
			if (html == null)
				throw new ArgumentNullException("html");

			if (node == null)
				throw new ArgumentNullException("node");

			var contextControllerName = html.ViewContext.RouteData.Values["controller"].ToString();
			var contextActionName = html.ViewContext.RouteData.Values["action"].ToString();

			return string.Equals(contextControllerName, node.ControllerName, StringComparison.OrdinalIgnoreCase) && string.Equals(contextActionName, node.ActionName, StringComparison.OrdinalIgnoreCase);
		}
	}
}