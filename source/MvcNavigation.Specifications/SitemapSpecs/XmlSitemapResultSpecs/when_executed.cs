// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;
using Moq;
using MvcNavigation.Sitemap;
using MvcNavigation.Specifications.ExtensibilitySpecs.DynamicNodeSpecs;
using It = Machine.Specifications.It;

namespace MvcNavigation.Specifications.SitemapSpecs.XmlSitemapResultSpecs
{
	[Subject(typeof(XmlSitemapResult))]
	public class when_executed
	{
		static StringWriter string_writer;

		Because of = () =>
		{
			NavigationConfiguration.Initialise(new Node<TestController>(c => c.RootAction(),
			                                                            new Node<TestController>(c => c.Action1()),
			                                                            new Node<TestController>(c => c.Action2(),
			                                                                                     new Node<TestController>(c => c.ParameterisedAction(1, "2")),
			                                                                                     new DynamicNodeImpl<TestController>(c => c.Action3()))));

			string_writer = new StringWriter();

			var httpRequest = new Mock<HttpRequestBase>();
			httpRequest.SetupGet(hr => hr.Url).Returns(new Uri("http://www.example.com/"));

			var httpResponse = new Mock<HttpResponseBase>();
			httpResponse.Setup(hr => hr.ApplyAppPathModifier(Moq.It.IsAny<string>())).Returns((string s) => s);
			httpResponse.SetupGet(hr => hr.Output).Returns(string_writer);

			var httpContext = new Mock<HttpContextBase>();
			httpContext.SetupGet(hc => hc.Request).Returns(httpRequest.Object);
			httpContext.SetupGet(hc => hc.Response).Returns(httpResponse.Object);

			var routeData = new RouteData();
			var requestContext = new RequestContext(httpContext.Object, routeData);

			var viewData = new ViewDataDictionary();
			viewData["CurrentLevel"] = 1;
			viewData["MaxLevels"] = 1;
			var viewDataContainer = new Mock<IViewDataContainer>();
			viewDataContainer.SetupGet(vdc => vdc.ViewData).Returns(viewData);

			var routeCollection = new RouteCollection();
			routeCollection.MapRoute("1", "root", new { controller = "Test", action = "RootAction" });
			routeCollection.MapRoute("2", "action1", new { controller = "Test", action = "Action1" });
			routeCollection.MapRoute("3", "action2", new { controller = "Test", action = "Action2" });
			routeCollection.MapRoute("4", "action3", new { controller = "Test", action = "Action3" });
			routeCollection.MapRoute("5", "parameterised-action/{param1}/{param2}", new { controller = "Test", action = "ParameterisedAction" });

			var urlHelper = new UrlHelper(requestContext, routeCollection);

			var controllerContext = new ControllerContext(httpContext.Object, routeData, new TestController());

			new XmlSitemapResult(urlHelper).ExecuteResult(controllerContext);
		};

		Cleanup after = () => NavigationConfiguration.Initialise(null);

		It should_write_xml_declaration_to_response_output_stream =
			() => string_writer.ToString().ShouldStartWith("<?xml version=\"1.0\" encoding=\"utf-16\"?>");

		It should_write_xml_sitemap_to_response_output_stream =
			() => string_writer.ToString().ShouldEndWith(string.Format(
			                                                           "{0}<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">" +
			                                                           "{0}  <url>" +
			                                                           "{0}    <loc>http://www.example.com/root</loc>" +
			                                                           "{0}    <priority>1.0</priority>" +
			                                                           "{0}  </url>" +
			                                                           "{0}  <url>" +
			                                                           "{0}    <loc>http://www.example.com/action1</loc>" +
			                                                           "{0}    <priority>0.9</priority>" +
			                                                           "{0}  </url>" +
			                                                           "{0}  <url>" +
			                                                           "{0}    <loc>http://www.example.com/action2</loc>" +
			                                                           "{0}    <priority>0.9</priority>" +
			                                                           "{0}  </url>" +
			                                                           "{0}  <url>" +
			                                                           "{0}    <loc>http://www.example.com/parameterised-action/1/2</loc>" +
			                                                           "{0}    <priority>0.8</priority>" +
			                                                           "{0}  </url>" +
			                                                           "{0}  <url>" +
			                                                           "{0}    <loc>http://www.example.com/action3</loc>" +
			                                                           "{0}    <priority>0.8</priority>" +
			                                                           "{0}  </url>" +
			                                                           "{0}  <url>" +
			                                                           "{0}    <loc>http://www.example.com/action1</loc>" +
			                                                           "{0}    <priority>0.7</priority>" +
			                                                           "{0}  </url>" +
			                                                           "{0}  <url>" +
			                                                           "{0}    <loc>http://www.example.com/action2</loc>" +
			                                                           "{0}    <priority>0.7</priority>" +
			                                                           "{0}  </url>" +
			                                                           "{0}</urlset>", Environment.NewLine)
			      	);
	}
}