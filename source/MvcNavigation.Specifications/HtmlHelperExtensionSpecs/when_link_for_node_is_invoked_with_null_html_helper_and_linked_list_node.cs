using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace MvcNavigation.Specifications.HtmlHelperExtensionSpecs
{
	[Subject(typeof(HtmlHelperExtensions))]
	public class when_link_for_node_is_invoked_with_null_html_helper_and_linked_list_node : action_link_spec
	{
		static Exception exception;

		Because of = () => exception = Catch.Exception(() => HtmlHelperExtensions.ActionLink(null, new LinkedListNode<INode>(null)));

		It should_throw_argument_null_exception =
			() => exception.GetType().ShouldEqual(typeof(ArgumentNullException));

		It should_throw_exception_with_descriptive_message =
			() => exception.Message.ShouldEqual(string.Format("Value cannot be null.{0}Parameter name: html", Environment.NewLine));
	}
}