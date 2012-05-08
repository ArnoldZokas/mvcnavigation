using Machine.Specifications;

namespace MvcNavigation.Specifications.NodeSpecs
{
	[Subject(typeof(Node<,>))]
	public class when_initialised_with_area_registration_and_custom_title
	{
		static Node<TestController, TestAreaRegistration> node;

		Because of = () => node = new Node<TestController, TestAreaRegistration>(c => c.DecoratedAction(), title: "Custom Title");

		It should_set_title_to_custom_title =
			() => node.Title.ShouldEqual("Custom Title");
	}
}