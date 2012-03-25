// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using System.Collections.Generic;
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
			return Menu(html, maxLevels: 1, renderAllLevels: false);
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

			var htmlAttributes = new Dictionary<string, object>();
			if (IsCurrentNode(html, linkTarget) || (IsRootNode(linkTarget) == false && IsAncestorOfCurrentNode(html, linkTarget)))
				htmlAttributes.Add("class", NavigationConfiguration.SelectedNodeCssClass);

			return html.ActionLink(linkTarget.Title, linkTarget.ActionName, linkTarget.ControllerName, linkTarget.RouteValues, htmlAttributes);
		}

		public static bool IsCurrentNode(this HtmlHelper html, INode node)
		{
			if (html == null)
				throw new ArgumentNullException("html");

			if (node == null)
				throw new ArgumentNullException("node");

			var viewContext = GetViewContext(html);
			var contextControllerName = GetContextControllerName(viewContext);
			var contextActionName = GetContextActionName(viewContext);
			var additionalRouteData = GetAdditionalRouteData(viewContext);

			return IsCurrentNode(node, contextControllerName, contextActionName, additionalRouteData);
		}

		public static bool IsAncestorOfCurrentNode(this HtmlHelper html, INode node)
		{
			if (html == null)
				throw new ArgumentNullException("html");

			if (node == null)
				throw new ArgumentNullException("node");

			var viewContext = GetViewContext(html);
			var contextControllerName = GetContextControllerName(viewContext);
			var contextActionName = GetContextActionName(viewContext);
			var additionalRouteData = GetAdditionalRouteData(viewContext);

			return node.Children.Any() && HasMatchingDescendant(node, contextControllerName, contextActionName, additionalRouteData, (int)html.ViewData["CurrentLevel"], (int)html.ViewData["MaxLevels"]);
		}

		static bool HasMatchingDescendant(INode currentNode, string contextControllerName, string contextActionName, IEnumerable<KeyValuePair<string, object>> additionalRouteData, int currentLevel, int maxLevels)
		{
			// check immediate children for match
			if (currentNode.Children.Any(childNode => IsCurrentNode(childNode, contextControllerName, contextActionName, additionalRouteData)))
				return true;

			// ensure scan depth does not exceed specified scan range
			if (currentLevel + 1 > maxLevels)
				return false;

			// check descendants
			return currentNode.Children.Any(childNode => HasMatchingDescendant(childNode, contextControllerName, contextActionName, additionalRouteData, currentLevel++, maxLevels));
		}

		static bool IsCurrentNode(INode node, string contextControllerName, string contextActionName, IEnumerable<KeyValuePair<string, object>> additionalRouteData)
		{
			if (string.Equals(contextControllerName, node.ControllerName, StringComparison.OrdinalIgnoreCase) == false || string.Equals(contextActionName, node.ActionName, StringComparison.OrdinalIgnoreCase) == false)
				return false;

			return additionalRouteData.All(routeDataItem => string.Equals(routeDataItem.Value.ToString(), node.RouteValues[routeDataItem.Key].ToString(), StringComparison.OrdinalIgnoreCase));
		}

		static ViewContext GetViewContext(HtmlHelper html)
		{
			return html.ViewContext.IsChildAction ? html.ViewContext.ParentActionViewContext : html.ViewContext;
		}

		static string GetContextControllerName(ViewContext viewContext)
		{
			return viewContext.RouteData.Values[RouteDataKeys.Controller].ToString();
		}

		static string GetContextActionName(ViewContext viewContext)
		{
			return viewContext.RouteData.Values[RouteDataKeys.Action].ToString();
		}

		static IEnumerable<KeyValuePair<string, object>> GetAdditionalRouteData(ViewContext viewContext)
		{
			return viewContext.RouteData.Values.Where(v => v.Key != RouteDataKeys.Controller && v.Key != RouteDataKeys.Action);
		}

		static bool IsRootNode(INode node)
		{
			return NavigationConfiguration.Sitemap == node;
		}

		#region Nested type: RouteDataKeys

		internal static class RouteDataKeys
		{
			public const string Action = "action";
			public const string Controller = "controller";
		}

		#endregion
	}
}