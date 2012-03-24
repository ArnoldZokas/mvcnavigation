// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Web.Mvc;
using Machine.Specifications;

namespace MvcNavigation.Specifications.HtmlHelperExtensionSpecs
{
	[Subject(typeof(HtmlHelperExtensions))]
	public class when_link_for_parameterised_node_that_does_not_match_current_parameter_is_requested : action_link_spec
	{
		static MvcHtmlString link;

		Because of = () =>
		{
			view_context.RouteData.Values.Add("controller", "Test");
			view_context.RouteData.Values.Add("action", "ParameterisedAction");
			view_context.RouteData.Values.Add("param1", "2");

			route_collection.MapRoute("Test", "action/{param1}", new { controller = "Test", action = "ParameterisedAction" });

			link = html_helper.ActionLink(new Node<TestController>(c => c.ParameterisedAction(1, "test")));
		};

		It should_generate_link_with_no_marker_css_class =
			() => link.ToString().ShouldEqual("<a href=\"/action/1?param2=test\">ParameterisedAction</a>");
	}
}