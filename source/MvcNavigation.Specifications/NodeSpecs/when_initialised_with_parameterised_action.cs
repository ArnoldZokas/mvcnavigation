using Machine.Specifications;

namespace MvcNavigation.Specifications.NodeSpecs
{
	[Subject(typeof(Node<>))]
	public class when_initialised_with_parameterised_action
	{
		static Node<TestController> node;

		Because of = () => node = new Node<TestController>(c => c.ParameterisedAction(1, "test"));

		It should_contain_route_value_with_name_param1_and_value_1 =
			() => node.RouteValues["param1"].ShouldEqual(1);

		It should_contain_route_value_with_name_param2_and_value_test =
			() => node.RouteValues["param2"].ShouldEqual("test");
	}
}