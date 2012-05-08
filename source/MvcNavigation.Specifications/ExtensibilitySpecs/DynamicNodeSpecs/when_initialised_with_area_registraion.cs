using Machine.Specifications;
using MvcNavigation.Extensibility;

namespace MvcNavigation.Specifications.ExtensibilitySpecs.DynamicNodeSpecs
{
	[Subject(typeof(DynamicNode<,>))]
	public class when_initialised_with_area_registraion
	{
		static DynamicNode<TestController, TestAreaRegistration> node;

		Because of = () => node = new AreaAwareDynamicNodeImpl<TestController, TestAreaRegistration>(c => c.RootAction());

		It should_set_area_name =
			() => node.AreaName.ShouldEqual("Test");
	}
}