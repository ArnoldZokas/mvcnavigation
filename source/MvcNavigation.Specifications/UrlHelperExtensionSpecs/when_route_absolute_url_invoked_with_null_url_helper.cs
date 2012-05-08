using System;
using Machine.Specifications;

namespace MvcNavigation.Specifications.UrlHelperExtensionSpecs
{
	[Subject(typeof(UrlHelperExtensions))]
	public class when_route_absolute_url_invoked_with_null_url_helper
	{
		static Exception exception;

		Because of = () => exception = Catch.Exception(() => UrlHelperExtensions.RouteAbsoluteUrl(null, new Node<TestController>(c => c.Action1())));

		It should_throw_argument_null_exception =
			() => exception.GetType().ShouldEqual(typeof(ArgumentNullException));

		It should_throw_exception_with_descriptive_message =
			() => exception.Message.ShouldEqual(string.Format("Value cannot be null.{0}Parameter name: url", Environment.NewLine));
	}
}