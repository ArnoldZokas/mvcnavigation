// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace MvcNavigation.Specifications.HtmlHelperExtensionSpecs
{
	[Subject(typeof(HtmlHelperExtensions))]
	public class when_link_for_node_is_requested
	{
		static MvcHtmlString link;

		Because of = () =>
		{
			var httpRequestBase = new Mock<HttpRequestBase>();
			
			var httpResponseBase = new Mock<HttpResponseBase>();
			httpResponseBase.Setup(hrb => hrb.ApplyAppPathModifier(Moq.It.IsAny<string>())).Returns((string s) => s);
			
			var httpContextBase = new Mock<HttpContextBase>();
			httpContextBase.SetupGet(hcb => hcb.Request).Returns(httpRequestBase.Object);
			httpContextBase.SetupGet(hcb => hcb.Response).Returns(httpResponseBase.Object);

			var requestContext = new RequestContext(httpContextBase.Object, new RouteData());
			var viewContext = new ViewContext { RequestContext = requestContext };
			
			var viewDataContainer = new Mock<IViewDataContainer>();

			var routeCollection = new RouteCollection();
			routeCollection.MapRoute("Test", "action1", new { controller = "Test", action = "Action1" });

			var htmlHelper = new HtmlHelper(viewContext, viewDataContainer.Object, routeCollection);
			
			link = htmlHelper.ActionLink(new Node<TestController>(c => c.Action1()));
		};

		It should_generate_link =
			() => link.ToString().ShouldEqual("<a href=\"/action1\">Action1</a>");
	}
}