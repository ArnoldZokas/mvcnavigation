// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using Machine.Specifications;
using MvcNavigation.Extensibility;

namespace MvcNavigation.Specifications.ExtensibilitySpecs.DynamicNodeSpecs
{
	[Subject(typeof(DynamicNode<>))]
	public class when_initialised_with_action_decorated_with_action_name_attribute
	{
		static DynamicNode<TestController> node;

		Because of = () => node = new DynamicNodeImpl<TestController>(c => c.DecoratedAction());

		It should_set_title_to_action_name_attribute_value =
			() => node.Title.ShouldEqual("Action");

		It should_set_action_name_to_action_name_attribute_value =
			() => node.ActionName.ShouldEqual("Action");
	}
}