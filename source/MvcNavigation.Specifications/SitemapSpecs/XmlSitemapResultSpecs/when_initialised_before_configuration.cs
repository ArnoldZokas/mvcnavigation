// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using Machine.Specifications;
using MvcNavigation.Sitemap;

namespace MvcNavigation.Specifications.SitemapSpecs.XmlSitemapResultSpecs
{
	[Subject(typeof(XmlSitemapResult))]
	public class when_initialised_before_configuration
	{
		static Exception exception;

		Because of = () => { exception = Catch.Exception(() => new XmlSitemapResult(null)); };

		It should_throw_exception_with_descriptive_message =
			() => exception.Message.ShouldEqual("MvcNavigation is not initialised.");

		It should_throw_invalid_operation_exception =
			() => exception.GetType().ShouldEqual(typeof(InvalidOperationException));
	}
}