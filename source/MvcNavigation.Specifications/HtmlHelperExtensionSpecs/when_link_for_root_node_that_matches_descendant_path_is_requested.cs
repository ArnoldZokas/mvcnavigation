// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Web.Mvc;
using Machine.Specifications;

namespace MvcNavigation.Specifications.HtmlHelperExtensionSpecs
{
	[Subject(typeof(HtmlHelperExtensions))]
	public class when_link_for_root_node_that_matches_descendant_path_is_requested : action_link_spec
	{
		static MvcHtmlString link;

		Because of = () =>
		{
			NavigationConfiguration.Initialise(new Node<TestController>(c => c.RootAction(),
			                                                            new Node<TestController>(c => c.Action1())));

			view_context.RouteData.Values.Add("controller", "Test");
			view_context.RouteData.Values.Add("action", "Action1");

			view_data.Add("CurrentLevel", 1);
			view_data.Add("MaxLevels", 1);

			route_collection.MapRoute("Test", "root", new { controller = "Test", action = "RootAction" });

			link = html_helper.ActionLink(NavigationConfiguration.Sitemap);
		};

		It should_generate_link_with_marker_css_class =
			() => link.ToString().ShouldEqual("<a href=\"/root\">RootAction</a>");
	}
}