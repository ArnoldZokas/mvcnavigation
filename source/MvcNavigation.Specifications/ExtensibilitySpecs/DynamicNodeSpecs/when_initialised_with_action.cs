// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Linq;
using Machine.Specifications;
using MvcNavigation.Extensibility;
using MvcNavigation.Specifications.SpecUtils;

namespace MvcNavigation.Specifications.ExtensibilitySpecs.DynamicNodeSpecs
{
	[Subject(typeof(DynamicNode<>))]
	public class when_initialised_with_action
	{
		static DynamicNode<TestController> node;

		Because of = () => node = new DynamicNodeImpl<TestController>(c => c.RootAction());

		It should_contain_action_info =
			() => node.ActionInfo.ShouldEqual(This.Action<TestController>(c => c.RootAction()));

		It should_set_controller_name =
			() => node.ControllerName.ShouldEqual("Test");

		It should_contain_generated_child_nodes =
			() => node.Children.Count().ShouldEqual(2);

		It should_set_parent_reference_on_child_nodes =
			() => node.Children.All(child => child.Parent == node).ShouldBeTrue();
	}
}