using System.Web.Mvc;
using Machine.Specifications;

namespace MvcNavigation.Specifications.HtmlHelperExtensionSpecs
{
	[Subject(typeof(HtmlHelperExtensions))]
	public class when_link_for_node_that_matches_descendant_path_is_requested : action_link_spec
	{
		static MvcHtmlString link;

		Because of = () =>
		{
			var rootNode = new Node<TestController>(c => c.RootAction(),
			                                        new Node<TestController>(c => c.Action1()),
			                                        new Node<TestController>(c => c.Action2(),
			                                                                 new Node<TestController>(c => c.Action3())));

			view_context.RouteData.Values.Add("controller", "Test");
			view_context.RouteData.Values.Add("action", "RootAction");

			view_data.Add("CurrentLevel", 1);
			view_data.Add("MaxLevels", 2);

			route_collection.MapRoute("Test", "root", new { controller = "Test", action = "RootAction" });

			link = html_helper.ActionLink(rootNode);
		};

		It should_generate_link_with_marker_css_class =
			() => link.ToString().ShouldEqual("<a class=\"selected\" href=\"/root\">RootAction</a>");
	}
}