using System;
using System.Web.Mvc;
using Machine.Specifications;

namespace MvcNavigation.Specifications.HtmlHelperExtensionSpecs
{
	[Subject(typeof(HtmlHelperExtensions))]
	public class when_menu_is_requested_before_configuration
	{
		static Exception exception;

		Because of = () =>
		{
			NavigationConfiguration.Initialise(null);
			var htmlHelper = new HtmlHelper(new ViewContext(), new ViewPage());
			exception = Catch.Exception(() => htmlHelper.Menu());
		};

		It should_throw_invalid_operation_exception =
			() => exception.GetType().ShouldEqual(typeof(InvalidOperationException));

		It should_throw_exception_with_descriptive_message =
			() => exception.Message.ShouldEqual("MvcNavigation is not initialised.");
	}
}