using System.Web.Mvc;
using Machine.Specifications;
using MvcNavigation.Sitemap;

namespace MvcNavigation.Specifications.SitemapSpecs.XmlSitemapControllerSpecs
{
	[Subject(typeof(XmlSitemapController))]
	public class when_sitemap_is_requested
	{
		static ActionResult result;

		Because of = () =>
		{
			NavigationConfiguration.Initialise(new Node<TestController>(c => c.RootAction()));

			result = new XmlSitemapController().Sitemap();
		};

		Cleanup after = () => NavigationConfiguration.Initialise(null);

		It should_return_xml_sitemap_result =
			() => result.ShouldBeOfType<XmlSitemapResult>();
	}
}