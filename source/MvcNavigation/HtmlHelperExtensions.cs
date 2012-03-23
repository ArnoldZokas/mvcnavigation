// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MvcNavigation.Configuration.Advanced;

namespace MvcNavigation
{
	public static class HtmlHelperExtensions
	{
		public static MvcHtmlString Menu(this HtmlHelper html)
		{
			return Menu(html, maxLevels: 1, renderAllLevels:false);
		}

		public static MvcHtmlString Menu(this HtmlHelper html, int maxLevels)
		{
			return Menu(html, maxLevels, renderAllLevels: false);
		}

		public static MvcHtmlString Menu(this HtmlHelper html, int maxLevels, bool renderAllLevels)
		{
			if (NavigationConfiguration.Sitemap == null)
				throw new InvalidOperationException("MvcNavigation is not initialised.");

			var rootNode = NavigationConfiguration.Sitemap;
			return RendererConfiguration.MenuRenderer(html, rootNode, maxLevels, renderAllLevels);
		}

		public static MvcHtmlString ActionLink(this HtmlHelper html, INode linkTarget)
		{
			if (html == null)
				throw new ArgumentNullException("html");

			if (linkTarget == null)
				throw new ArgumentNullException("linkTarget");

			object htmlAttributes = null;
			if (IsCurrentNode(html, linkTarget) || (IsRootNode(linkTarget) == false && IsAncestorOfCurrentNode(html, linkTarget)))
				htmlAttributes = new { @class = NavigationConfiguration.CurrentNodeCssClass };

			return html.ActionLink(linkTarget.Title, linkTarget.ActionName, linkTarget.ControllerName, null, htmlAttributes);
		}

		public static bool IsCurrentNode(this HtmlHelper html, INode node)
		{
			if (html == null)
				throw new ArgumentNullException("html");

			if (node == null)
				throw new ArgumentNullException("node");

			var viewContext = html.ViewContext.IsChildAction ? html.ViewContext.ParentActionViewContext : html.ViewContext;
			var contextControllerName = viewContext.RouteData.Values["controller"].ToString();
			var contextActionName = viewContext.RouteData.Values["action"].ToString();

			return string.Equals(contextControllerName, node.ControllerName, StringComparison.OrdinalIgnoreCase) && string.Equals(contextActionName, node.ActionName, StringComparison.OrdinalIgnoreCase);
		}

		public static bool IsAncestorOfCurrentNode(this HtmlHelper html, INode node)
		{
			if (html == null)
				throw new ArgumentNullException("html");

			if (node == null)
				throw new ArgumentNullException("node");

			var viewContext = html.ViewContext.IsChildAction ? html.ViewContext.ParentActionViewContext : html.ViewContext;
			var contextControllerName = viewContext.RouteData.Values["controller"].ToString();
			var contextActionName = viewContext.RouteData.Values["action"].ToString();

			if (node.Children.Any() == false)
				return false;

			return HasMatchingDescendant(node, contextControllerName, contextActionName, (int)html.ViewData["CurrentLevel"], (int)html.ViewData["MaxLevels"]);
		}

		static bool IsRootNode(INode node)
		{
			return NavigationConfiguration.Sitemap == node;
		}

		static bool HasMatchingDescendant(INode currentNode, string contextControllerName, string contextActionName, int currentLevel, int maxLevels)
		{
			foreach (var childNode in currentNode.Children)
				if (string.Equals(contextControllerName, childNode.ControllerName, StringComparison.OrdinalIgnoreCase) && string.Equals(contextActionName, childNode.ActionName, StringComparison.OrdinalIgnoreCase))
					return true;

			if (currentLevel + 1 > maxLevels)
				return false;

			foreach (var childNode in currentNode.Children)
				if (HasMatchingDescendant(childNode, contextControllerName, contextActionName, currentLevel++, maxLevels))
					return true;

			return false;
		}
	}
}