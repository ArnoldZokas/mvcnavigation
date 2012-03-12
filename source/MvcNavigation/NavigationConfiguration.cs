// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

namespace MvcNavigation
{
	public class NavigationConfiguration
	{
		static NavigationConfiguration()
		{
			CurrentNodeCssClass = "current";
		}

		public static INode Sitemap { get; private set; }
		public static string CurrentNodeCssClass { get; set; }

		public static void Initialise(INode rootNode)
		{
			Sitemap = rootNode;
		}
	}
}