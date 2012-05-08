using System;
using Machine.Specifications;

namespace MvcNavigation.Specifications.HtmlHelperExtensionSpecs
{
	[Subject(typeof(HtmlHelperExtensions))]
	public class when_is_ancestor_of_current_node_is_invoked_with_null_html_helper : action_link_spec
	{
		static Exception exception;

		Because of = () => exception = Catch.Exception(() => HtmlHelperExtensions.IsAncestorOfCurrentNode(null, new Node<TestController>(c => c.Action1())));

		It should_throw_argument_null_exception =
			() => exception.GetType().ShouldEqual(typeof(ArgumentNullException));

		It should_throw_exception_with_descriptive_message =
			() => exception.Message.ShouldEqual(string.Format("Value cannot be null.{0}Parameter name: html", Environment.NewLine));
	}
}