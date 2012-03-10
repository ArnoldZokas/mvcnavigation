// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using Machine.Specifications;

namespace MvcNavigation.Specifications.NodeSpecs
{
	[Subject(typeof(Node<>))]
	public class when_initialised_with_null_expression
	{
		static Exception exception;

		Because of = () => exception = Catch.Exception(() => new Node<TestController>(null));

		It should_throw_sitemap_configuration_exception =
			() => exception.GetType().ShouldEqual(typeof(ArgumentNullException));

		It should_throw_exception_with_descriptive_message =
			() => exception.Message.ShouldEqual(string.Format("Value cannot be null.{0}Parameter name: action", Environment.NewLine));
	}
}