using Machine.Specifications;
using MvcNavigation.Sitemap;

namespace MvcNavigation.Specifications.SitemapSpecs.XmlSitemapResultSpecs
{
	[Subject(typeof(XmlSitemapResult))]
	public class when_initialised
	{
		static XmlSitemapResult xml_sitemap_result;

		Because of = () =>
		{
			NavigationConfiguration.Initialise(new Node<TestController>(c => c.RootAction()));

			xml_sitemap_result = new XmlSitemapResult(null);
		};

		Cleanup after = () => NavigationConfiguration.Initialise(null);

		It should_set_content_type_to_application_xml = () => xml_sitemap_result.ContentType.ShouldEqual("application/xml");
	}
}