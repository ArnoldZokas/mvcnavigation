using System.Web.Mvc;
using Machine.Specifications;

namespace MvcNavigation.Specifications.HtmlHelperExtensionSpecs
{
	[Subject(typeof(HtmlHelperExtensions))]
	public class when_link_for_node_that_matches_current_path_is_requested_from_child_view : action_link_spec
	{
		static MvcHtmlString link;

		Because of = () =>
		{
			var parentActionViewContext = new ViewContext();
			parentActionViewContext.RouteData.Values.Add("controller", "Test");
			parentActionViewContext.RouteData.Values.Add("action", "Action1");
			route_data.DataTokens.Add("ParentActionViewContext", parentActionViewContext);

			route_collection.MapRoute("Test", "action1", new { controller = "Test", action = "Action1" });

			link = html_helper.ActionLink(new Node<TestController>(c => c.Action1()));
		};

		It should_generate_link_with_marker_css_class =
			() => link.ToString().ShouldEqual("<a class=\"selected\" href=\"/action1\">Action1</a>");
	}
}