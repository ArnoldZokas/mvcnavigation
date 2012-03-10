// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using Machine.Specifications;

namespace MvcNavigation.Specifications.NodeSpecs
{
	[Subject(typeof(Node<>))]
	public class when_initialised_with_2_child_nodes
	{
		static Node<TestController> node;

		Because of = () => node = new Node<TestController>(c => c.RootAction(),
		                                                   new Node<TestController>(c => c.Action1()),
		                                                   new Node<TestController>(c => c.Action2()));

		It should_contain_1_child_node =
			() => node.Children.Count.ShouldEqual(2);
	}
}