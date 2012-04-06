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
	public class when_breadcrumb_not_matching_current_path_is_requested
	{
		static MvcHtmlString menu;

		Because of = () =>
		{
			NavigationConfiguration.Initialise(new Node<TestController>(c => c.RootAction()));

			RendererConfiguration.BreadcrumbRenderer = (html, model) =>
			{
				const string template = "Count:@Model.Count";

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
			viewContext.RouteData.Values.Add("action", "Action1");

			var htmlHelper = new HtmlHelper(viewContext, new ViewPage());
			menu = htmlHelper.Breadcrumb();
		};

		It should_generate_menu =
			() => menu.ToString().ShouldEqual("Count:0");
	}
}