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
	}
}