// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Web.Mvc;
using Machine.Specifications;

namespace MvcNavigation.Specifications.HtmlHelperExtensionSpecs
{
	[Subject(typeof(HtmlHelperExtensions))]
	public class when_link_for_node_that_matches_current_path_is_requested : action_link_spec
	{
		static MvcHtmlString link;

		Because of = () =>
		{
			view_context.RouteData.Values.Add("controller", "Test");
			view_context.RouteData.Values.Add("action", "Action1");

			route_collection.MapRoute("Test", "action1", new { controller = "Test", action = "Action1" });

			link = html_helper.ActionLink(new Node<TestController>(c => c.Action1()));
		};

		It should_generate_link_with_marker_css_class =
			() => link.ToString().ShouldEqual("<a class=\"current\" href=\"/action1\">Action1</a>");
	}
}