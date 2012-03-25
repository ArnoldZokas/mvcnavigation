// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using Machine.Specifications;
using MvcNavigation.Specifications.SpecUtils;

namespace MvcNavigation.Specifications.NodeSpecs
{
	[Subject(typeof(Node<>))]
	public class when_initialised_with_action
	{
		static Node<TestController> node;

		Because of = () => node = new Node<TestController>(c => c.RootAction());

		It should_contain_action_info =
			() => node.ActionInfo.ShouldEqual(This.Action<TestController>(c => c.RootAction()));

		It should_set_controller_name =
			() => node.ControllerName.ShouldEqual("Test");

		It should_set_empty_area_name =
			() => node.AreaName.ShouldEqual("");

		It should_contain_empty_area_route_value =
			() => node.RouteValues["area"].ShouldEqual("");

		It should_generate_string_representation_with_child_count_0 =
			() => node.ToString().ShouldEqual("TestController.RootAction(), child count: 0");
	}
}