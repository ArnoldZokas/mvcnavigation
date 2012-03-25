// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using Machine.Specifications;

namespace MvcNavigation.Specifications.NodeSpecs
{
	[Subject(typeof(Node<,>))]
	public class when_initialised_with_area_registraion
	{
		static Node<TestController, TestAreaRegistration> node;

		Because of = () => node = new Node<TestController, TestAreaRegistration>(c => c.RootAction());

		It should_set_area_name =
			() => node.AreaName.ShouldEqual("Test");
	}
}