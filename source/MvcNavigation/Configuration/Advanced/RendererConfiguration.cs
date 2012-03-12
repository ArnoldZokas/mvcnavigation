// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MvcNavigation.Internal;

namespace MvcNavigation.Configuration.Advanced
{
	public static class RendererConfiguration
	{
		static RendererConfiguration()
		{
			MenuRenderer = (html, model) =>
			{
				var modelHtmlHelper = new HtmlHelper<INode>(html.ViewContext, new ViewDataContainer<INode>(model));
				return modelHtmlHelper.DisplayFor(node => node, "MvcNavigationMenuRoot");
			};
		}

		public static Func<HtmlHelper, INode, MvcHtmlString> MenuRenderer { get; set; }
	}
}