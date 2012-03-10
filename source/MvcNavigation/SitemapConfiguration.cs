// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

namespace MvcNavigation
{
	public class SitemapConfiguration
	{
		public static INode Sitemap { get; private set; }

		public static void Initialise(INode rootNode)
		{
			Sitemap = rootNode;
		}
	}
}