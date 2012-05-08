using Machine.Specifications;
using MvcNavigation.Extensibility;

namespace MvcNavigation.Specifications.ExtensibilitySpecs.DynamicNodeSpecs
{
	[Subject(typeof(DynamicNode<>))]
	public class when_initialised_with_title_custom_title
	{
		static DynamicNode<TestController> node;

		Because of = () => node = new DynamicNodeImpl<TestController>(c => c.DecoratedAction(), title: "Custom Title");

		It should_set_title_to_custom_title =
			() => node.Title.ShouldEqual("Custom Title");
	}
}