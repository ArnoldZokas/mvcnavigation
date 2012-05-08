using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MvcNavigation.Internal;

namespace MvcNavigation.Configuration.Advanced
{
	public static class RendererConfiguration
	{
		static RendererConfiguration()
		{
			MenuRenderer = (html, model, maxLevels, renderAllLevels) =>
			{
				var modelHtmlHelper = new HtmlHelper<INode>(html.ViewContext, new ViewDataContainer<INode>(model));
				return modelHtmlHelper.DisplayFor(node => node,
				                                  "MvcNavigationMenuRoot",
				                                  new
				                                  {
				                                  	CurrentLevel = 1,
				                                  	MaxLevels = maxLevels,
				                                  	RenderAllLevels = renderAllLevels
				                                  });
			};

			BreadcrumbRenderer = (html, model) =>
			{
				var modelHtmlHelper = new HtmlHelper<LinkedList<INode>>(html.ViewContext, new ViewDataContainer<LinkedList<INode>>(model));
				return modelHtmlHelper.DisplayFor(node => node,
				                                  "MvcNavigationBreadcrumb",
				                                  new { CurrentLevel = 1 });
			};
		}

		public static Func<HtmlHelper, INode, int, bool, MvcHtmlString> MenuRenderer { get; set; }
		public static Func<HtmlHelper, LinkedList<INode>, MvcHtmlString> BreadcrumbRenderer { get; set; }
	}
}