// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using Machine.Specifications;

namespace MvcNavigation.Specifications.NavigationConfigurationSpecs
{
	[Subject(typeof(NavigationConfiguration))]
	public class when_initialised_with_name
	{
		Because of = () => NavigationConfiguration.Initialise("Name", new Node<TestController>(c => c.RootAction()));

		It should_contain_named_sitemap =
			() => NavigationConfiguration.GetSitemap("Name").ShouldNotBeNull();
	}
}