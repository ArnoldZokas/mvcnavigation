using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;

namespace MvcNavigation.Specifications.HtmlHelperExtensionSpecs
{
	[Subject(typeof(HtmlHelperExtensions))]
	public class when_link_for_node_that_uses_area_registration_is_requested : action_link_spec
	{
		static MvcHtmlString link;

		Because of = () =>
		{
			view_context.RouteData.Values.Add("controller", "");
			view_context.RouteData.Values.Add("action", "");

			route_collection.MapRoute("Test", "action1", new { controller = "Test", action = "Action1" });
			((Route)route_collection.Last()).DataTokens.Add("area", "Test"); // emulates area registration behaviour

			link = html_helper.ActionLink(new Node<TestController, TestAreaRegistration>(c => c.Action1()));
		};

		It should_generate_link =
			() => link.ToString().ShouldEqual("<a href=\"/action1\">Action1</a>");
	}
}