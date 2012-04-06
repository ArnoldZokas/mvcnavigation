// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace MvcNavigation.Specifications.UrlHelperExtensionSpecs
{
	[Subject(typeof(UrlHelperExtensions))]
	public class when_route_absolute_url_invoked_with_null_node
	{
		static Exception exception;

		Because of = () =>
		{
			var httpContext = new Mock<HttpContextBase>();
			var requestContext = new RequestContext(httpContext.Object, new RouteData());
			var routeCollection = new RouteCollection();
			var urlHelper = new UrlHelper(requestContext, routeCollection);

			exception = Catch.Exception(() => urlHelper.RouteAbsoluteUrl(null));
		};

		It should_throw_argument_null_exception =
			() => exception.GetType().ShouldEqual(typeof(ArgumentNullException));

		It should_throw_exception_with_descriptive_message =
			() => exception.Message.ShouldEqual(string.Format("Value cannot be null.{0}Parameter name: node", Environment.NewLine));
	}
}