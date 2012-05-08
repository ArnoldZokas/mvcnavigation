using Machine.Specifications;

namespace MvcNavigation.Specifications.NodeSpecs
{
	[Subject(typeof(Node<>))]
	public class when_initialised_with_action_decorated_with_action_name_attribute
	{
		static Node<TestController> node;

		Because of = () => node = new Node<TestController>(c => c.DecoratedAction());

		It should_set_title_to_action_name_attribute_value =
			() => node.Title.ShouldEqual("Action");

		It should_set_action_name_to_action_name_attribute_value =
			() => node.ActionName.ShouldEqual("Action");
	}
}