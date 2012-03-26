// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Collections.Generic;

namespace MvcNavigation
{
	public class NavigationConfiguration
	{
		const string DefaultSitemapName = "Default";
		static readonly Dictionary<string, INode> Sitemaps = new Dictionary<string, INode>();

		static NavigationConfiguration()
		{
			SelectedNodeCssClass = "selected";
		}

		public static INode DefaultSitemap { get; private set; }
		public static string SelectedNodeCssClass { get; set; }

		public static void Initialise(INode rootNode)
		{
			Sitemaps[DefaultSitemapName] = rootNode;
			DefaultSitemap = rootNode;
		}

		public static void Initialise(string name, INode rootNode)
		{
			Sitemaps[name] = rootNode;
		}

		public static INode GetSitemap(string name)
		{
			return Sitemaps.ContainsKey(name) ? Sitemaps[name] : null;
		}
	}
}