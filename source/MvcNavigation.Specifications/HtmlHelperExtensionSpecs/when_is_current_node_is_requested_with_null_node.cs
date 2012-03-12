// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using Machine.Specifications;

namespace MvcNavigation.Specifications.HtmlHelperExtensionSpecs
{
	[Subject(typeof(HtmlHelperExtensions))]
	public class when_is_current_node_is_requested_with_null_node : action_link_spec
	{
		static Exception exception;

		Because of = () => exception = Catch.Exception(() => html_helper.IsCurrentNode(null));

		It should_throw_argument_null_exception =
			() => exception.GetType().ShouldEqual(typeof(ArgumentNullException));

		It should_throw_exception_with_descriptive_message =
			() => exception.Message.ShouldEqual(string.Format("Value cannot be null.{0}Parameter name: node", Environment.NewLine));
	}
}