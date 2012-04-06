// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Collections.Generic;
using System.Dynamic;
using System.Web.Mvc;
using Machine.Specifications;
using Microsoft.CSharp.RuntimeBinder;
using MvcNavigation.Configuration.Advanced;
using MvcNavigation.Specifications.SpecUtils;

namespace MvcNavigation.Specifications.HtmlHelperExtensionSpecs
{
	[Subject(typeof(HtmlHelperExtensions))]
	public class when_named_breadcrumb_is_requested
	{
		static MvcHtmlString menu;

		Because of = () =>
		{
			NavigationConfiguration.Initialise(
			                                   new Node<TestController>(c => c.RootAction(),
			                                                            new Node<TestController>(c => c.ParameterisedAction(2, "2"))));
			NavigationConfiguration.Initialise(
			                                   "NamedSitemap",
			                                   new Node<TestController>(c => c.RootAction(),
			                                                            new Node<TestController>(c => c.Action1()),
			                                                            new Node<TestController>(c => c.Action2(),
			                                                                                     new Node<TestController>(c => c.ParameterisedAction(1, "1")),
			                                                                                     new Node<TestController>(c => c.ParameterisedAction(2, "2")),
			                                                                                     new Node<TestController>(c => c.ParameterisedAction(3, "3")))));

			RendererConfiguration.BreadcrumbRenderer = (html, model) =>
			{
				const string template = "Title:@Model.First.Value.Title, Count:@Model.Count";

				var referenceAssemblies = new[]
				{
					typeof(LinkedList<>).Assembly, // System
					typeof(ExpandoObject).Assembly, // System.Core
					typeof(Binder).Assembly, // Microsoft.CSharp
					typeof(INode).Assembly // MvcNavigation
				};
				var executionResult = InMemoryRazorEngine.Execute(template, model, null, referenceAssemblies);
				return new MvcHtmlString(executionResult.RuntimeResult);
			};

			var viewContext = new ViewContext();
			viewContext.RouteData.Values.Add("controller", "Test");
			viewContext.RouteData.Values.Add("action", "ParameterisedAction");
			viewContext.RouteData.Values.Add("param1", "2");
			viewContext.RouteData.Values.Add("param2", "2");

			var htmlHelper = new HtmlHelper(viewContext, new ViewPage());
			menu = htmlHelper.Breadcrumb("NamedSitemap");
		};

		It should_generate_menu =
			() => menu.ToString().ShouldEqual("Title:RootAction, Count:3");
	}
}