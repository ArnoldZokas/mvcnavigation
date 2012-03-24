// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using Machine.Specifications;

namespace MvcNavigation.Specifications.NodeSpecs
{
	[Subject(typeof(Node<>))]
	public class when_initialised_with_custom_title
	{
		static Node<TestController> node;

		Because of = () => node = new Node<TestController>(c => c.DecoratedAction(), title: "Custom Title");

		It should_set_title_to_custom_title =
			() => node.Title.ShouldEqual("Custom Title");
	}
}