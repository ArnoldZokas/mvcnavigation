using Machine.Specifications;

namespace MvcNavigation.Specifications.HtmlHelperExtensionSpecs
{
	[Subject(typeof(HtmlHelperExtensions))]
	public class when_is_ancestor_of_current_node_is_invoked_with_node_that_is_not_an_ancestor : action_link_spec
	{
		static bool result;

		Because of = () =>
		{
			view_context.RouteData.Values.Add("controller", "Test");
			view_context.RouteData.Values.Add("action", "Action2");

			view_data.Add("CurrentLevel", 1);
			view_data.Add("MaxLevels", 2);

			result = html_helper.IsAncestorOfCurrentNode(new Node<TestController>(c => c.Action1()));
		};

		It should_return_false =
			() => result.ShouldBeFalse();
	}
}