// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using Machine.Specifications;

namespace MvcNavigation.Specifications.NavigationConfigurationSpecs
{
	[Subject(typeof(NavigationConfiguration))]
	public class when_initialised
	{
		Because of = () => NavigationConfiguration.Initialise(new Node<TestController>(c => c.RootAction()));

		It should_contain_root_sitemap_node =
			() => NavigationConfiguration.Sitemap.ShouldNotBeNull();
	}
}