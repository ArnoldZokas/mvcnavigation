// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;
using Moq;
using It = Moq.It;

namespace MvcNavigation.Specifications.HtmlHelperExtensionSpecs
{
	public abstract class action_link_spec
	{
		protected static RouteData route_data;
		protected static ViewContext view_context;
		protected static RouteCollection route_collection;
		protected static HtmlHelper html_helper;

		Establish context = () =>
		{
			var httpRequestBase = new Mock<HttpRequestBase>();

			var httpResponseBase = new Mock<HttpResponseBase>();
			httpResponseBase.Setup(hrb => hrb.ApplyAppPathModifier(It.IsAny<string>())).Returns((string s) => s);

			var httpContextBase = new Mock<HttpContextBase>();
			httpContextBase.SetupGet(hcb => hcb.Request).Returns(httpRequestBase.Object);
			httpContextBase.SetupGet(hcb => hcb.Response).Returns(httpResponseBase.Object);

			route_data = new RouteData();
			var requestContext = new RequestContext(httpContextBase.Object, route_data);
			view_context = new ViewContext { RequestContext = requestContext };
			var viewDataContainer = new Mock<IViewDataContainer>();
			route_collection = new RouteCollection();

			html_helper = new HtmlHelper(view_context, viewDataContainer.Object, route_collection);
		};
	}
}