using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Policy;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MvcNavigation.Configuration.Advanced;
using MvcNavigation.Internal;

namespace MvcNavigation
{
	public static class HtmlHelperExtensions
	{
		#region Menu

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
			if (NavigationConfiguration.DefaultSitemap == null)
				throw new InvalidOperationException("MvcNavigation is not initialised.");

			var rootNode = NavigationConfiguration.DefaultSitemap;
			return RendererConfiguration.MenuRenderer(html, rootNode, maxLevels, renderAllLevels);
		}

		public static MvcHtmlString Menu(this HtmlHelper html, string name)
		{
			return Menu(html, name, maxLevels: 1, renderAllLevels: false);
		}

		public static MvcHtmlString Menu(this HtmlHelper html, string name, int maxLevels)
		{
			return Menu(html, name, maxLevels, renderAllLevels: false);
		}

		public static MvcHtmlString Menu(this HtmlHelper html, string name, int maxLevels, bool renderAllLevels)
		{
			var namedSitemap = NavigationConfiguration.GetSitemap(name);
			if (namedSitemap == null)
				throw new InvalidOperationException(string.Format("Sitemap \"{0}\" is not initialised.", name));

			return RendererConfiguration.MenuRenderer(html, namedSitemap, maxLevels, renderAllLevels);
		}

		#endregion

		#region Breadcrumb

		public static MvcHtmlString Breadcrumb(this HtmlHelper html)
		{
			if (NavigationConfiguration.DefaultSitemap == null)
				throw new InvalidOperationException("MvcNavigation is not initialised.");

			var breadcrumbTrail = ConstructBreadcrumbTrail(html, NavigationConfiguration.DefaultSitemap);
			return RendererConfiguration.BreadcrumbRenderer(html, breadcrumbTrail);
		}

		public static MvcHtmlString Breadcrumb(this HtmlHelper html, string name)
		{
			var namedSitemap = NavigationConfiguration.GetSitemap(name);
			if (namedSitemap == null)
				throw new InvalidOperationException(string.Format("Sitemap \"{0}\" is not initialised.", name));

			var breadcrumbTrail = ConstructBreadcrumbTrail(html, namedSitemap);
			return RendererConfiguration.BreadcrumbRenderer(html, breadcrumbTrail);
		}

		#endregion

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

      
		public static MvcHtmlString ActionLink(this HtmlHelper html, LinkedListNode<INode> linkTarget)
		{
			if (html == null)
				throw new ArgumentNullException("html");

			if (linkTarget == null)
				throw new ArgumentNullException("linkTarget");

			var node = linkTarget.Value;
			return html.ActionLink(node.Title, node.ActionName, node.ControllerName, node.RouteValues, null);
		}

        public static MvcHtmlString IconLinkFor(this HtmlHelper html, INode linkTarget, string cssClass)
        {
            if (html == null)
                throw new ArgumentNullException("html");

            if (linkTarget == null)
                throw new ArgumentNullException("linkTarget");

            if (!string.IsNullOrEmpty(linkTarget.IconFileName))
            {
                var link = new StringBuilder();
                var actionUrl = String.Format(@"/{0}/{1}", linkTarget.ControllerName, linkTarget.ActionName);
               
                if (!string.IsNullOrEmpty(cssClass))
                    link.Append(
                        String.Format(
                            @"<a class='{0}' href='{1}' data-state='{2}' title='{3}'><i class='{4}'></i><span>{5}</span><em></em></a>",
                            cssClass
                            , actionUrl
                            , linkTarget.ControllerName
                            , linkTarget.Title
                            , linkTarget.IconFileName
                            , linkTarget.Title));

                return new MvcHtmlString(link.ToString());
            }

            
            return new MvcHtmlString(String.Empty);
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
			var nextLevel = currentLevel + 1;
			return currentNode.Children.Any(childNode => HasMatchingDescendant(childNode, contextControllerName, contextActionName, additionalRouteData, nextLevel, maxLevels));
		}

		static bool IsCurrentNode(INode node, string contextControllerName, string contextActionName, IEnumerable<KeyValuePair<string, object>> additionalRouteData)
		{
			if (string.Equals(contextControllerName, node.ControllerName, StringComparison.OrdinalIgnoreCase) == false || string.Equals(contextActionName, node.ActionName, StringComparison.OrdinalIgnoreCase) == false)
				return false;

			return additionalRouteData.All(routeDataItem => string.Equals(routeDataItem.Value.ToString(), node.RouteValues[routeDataItem.Key] != null ? node.RouteValues[routeDataItem.Key].ToString() : null, StringComparison.OrdinalIgnoreCase));
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
			return node.Parent == null;
		}

		static LinkedList<INode> ConstructBreadcrumbTrail(HtmlHelper html, INode sitemap)
		{
			var current = FindNode(sitemap, node => IsCurrentNode(html, node));
			if (current == null)
				return new LinkedList<INode>();

			var breadcrumbTrail = new LinkedList<INode>();
			breadcrumbTrail.AddLast(current);
			while (current.Parent != null)
			{
				breadcrumbTrail.AddFirst(current.Parent);
				current = current.Parent;
			}

			return breadcrumbTrail;
		}

		static INode FindNode(INode startNode, Func<INode, bool> predicate)
		{
			var queue = new Queue<INode>();
			queue.Enqueue(startNode);

			while (queue.Count > 0)
			{
				var current = queue.Dequeue();

				if (predicate(current))
					return current;

				foreach (var childNode in current.Children)
					queue.Enqueue(childNode);
			}

			return null;
		}
	}
}
