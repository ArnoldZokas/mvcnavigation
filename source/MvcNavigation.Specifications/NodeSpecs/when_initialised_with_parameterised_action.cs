// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using Machine.Specifications;

namespace MvcNavigation.Specifications.NodeSpecs
{
	[Subject(typeof(Node<>))]
	public class when_initialised_with_parameterised_action
	{
		static Node<TestController> node;

		Because of = () => node = new Node<TestController>(c => c.ParameterisedAction(1, "test"));

		It should_contain_2_parameters =
			() => node.Arguments.Count.ShouldEqual(2);

		It should_contain_parameter_with_name_param1_and_value_1 =
			() => node.Arguments["param1"].ShouldEqual(1);

		It should_contain_parameter_with_name_param2_and_value_test =
			() => node.Arguments["param2"].ShouldEqual("test");
	}
}