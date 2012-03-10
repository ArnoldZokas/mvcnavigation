// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MvcNavigation
{
	public static class MenuHelper
	{
		static MenuHelper()
		{
			Renderer = (html, model) =>
			{
				var modelHtmlHelper = new HtmlHelper<INode>(html.ViewContext, new ViewDataContainer<INode>(model));
				return modelHtmlHelper.DisplayFor(node => node, "MenuRoot");
			};
		}

		public static Func<HtmlHelper, INode, MvcHtmlString> Renderer { get; set; }

		public static MvcHtmlString Menu(this HtmlHelper html)
		{
			var rootNode = SitemapConfiguration.Sitemap;
			return Renderer(html, rootNode);
		}
	}
}