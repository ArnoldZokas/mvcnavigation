﻿// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using Machine.Specifications;

namespace MvcNavigation.Specifications.NodeSpecs
{
	[Subject(typeof(Node<,>))]
	public class when_initialised_with_area_registration_and_1_child_node
	{
		static Node<TestController, TestAreaRegistration> node;

		Because of = () => node = new Node<TestController, TestAreaRegistration>(c => c.RootAction(), new Node<TestController>(c => c.Action1()));

		It should_contain_1_child_node =
			() => node.Children.Count.ShouldEqual(1);
	}
}