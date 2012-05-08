using System;
using Machine.Specifications;
using MvcNavigation.Extensibility;

namespace MvcNavigation.Specifications.ExtensibilitySpecs.DynamicNodeSpecs
{
	[Subject(typeof(DynamicNode<>))]
	public class when_initialised_with_null_expression
	{
		static Exception exception;

		Because of = () => exception = Catch.Exception(() => new DynamicNodeImpl<TestController>(null));

		It should_throw_argument_null_exception =
			() => exception.GetType().ShouldEqual(typeof(ArgumentNullException));

		It should_throw_exception_with_descriptive_message =
			() => exception.Message.ShouldEqual(string.Format("Value cannot be null.{0}Parameter name: action", Environment.NewLine));
	}
}