using System;
using Machine.Specifications;
using MvcNavigation.Extensibility;

namespace MvcNavigation.Specifications.ExtensibilitySpecs.DynamicNodeSpecs
{
	[Subject(typeof(DynamicNode<>))]
	public class when_initialised_with_invalid_expression
	{
		static Exception exception;

		Because of = () => exception = Catch.Exception(() => new DynamicNodeImpl<TestController>(c => new object()));

		It should_throw_argument_exception =
			() => exception.GetType().ShouldEqual(typeof(ArgumentException));

		It should_throw_exception_with_descriptive_message =
			() => exception.Message.ShouldEqual(string.Format("Node must be initialised with method call expression (e.g. controller => controller.Action()){0}Parameter name: action", Environment.NewLine));
	}
}