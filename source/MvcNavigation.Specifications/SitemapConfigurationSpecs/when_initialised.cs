// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using Machine.Specifications;

namespace MvcNavigation.Specifications.SitemapConfigurationSpecs
{
	[Subject(typeof(SitemapConfiguration))]
	public class when_initialised
	{
		Because of = () => SitemapConfiguration.Initialise(new Node<TestController>(c => c.RootAction()));

		It should_contain_root_sitemap_node =
			() => SitemapConfiguration.Sitemap.ShouldNotBeNull();
	}
}