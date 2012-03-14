// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using Machine.Specifications;

namespace MvcNavigation.Specifications.HtmlHelperExtensionSpecs
{
	[Subject(typeof(HtmlHelperExtensions))]
	public class when_is_ancestor_of_current_node_is_invoked_with_node_that_is_an_immediate_ancestor : action_link_spec
	{
		static bool result;

		Because of = () =>
		{
			var rootNode = new Node<TestController>(c => c.RootAction(),
			                                        new Node<TestController>(c => c.Action1()),
			                                        new Node<TestController>(c => c.Action2()));

			view_context.RouteData.Values.Add("controller", "Test");
			view_context.RouteData.Values.Add("action", "Action2");

			view_data.Add("CurrentLevel", 1);
			view_data.Add("MaxLevels", 2);

			result = html_helper.IsAncestorOfCurrentNode(rootNode);
		};

		It should_return_true =
			() => result.ShouldBeTrue();
	}
}