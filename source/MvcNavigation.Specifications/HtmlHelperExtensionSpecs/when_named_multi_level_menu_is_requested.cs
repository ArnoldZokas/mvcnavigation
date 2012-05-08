using System.Dynamic;
using System.Web.Mvc;
using Machine.Specifications;
using Microsoft.CSharp.RuntimeBinder;
using MvcNavigation.Configuration.Advanced;
using MvcNavigation.Specifications.SpecUtils;

namespace MvcNavigation.Specifications.HtmlHelperExtensionSpecs
{
	[Subject(typeof(HtmlHelperExtensions))]
	public class when_named_multi_level_menu_is_requested
	{
		static MvcHtmlString menu;

		Because of = () =>
		{
			NavigationConfiguration.Initialise(new Node<TestController>(c => c.RootAction()));
			NavigationConfiguration.Initialise("NamedSitemap", new Node<TestController>(c => c.Action1()));

			RendererConfiguration.MenuRenderer = (html, model, maxLevels, renderAllLevels) =>
			{
				const string template = "Title:@Model.Title, maxLevels:@ViewBag.MaxLevels, renderAllLevels:false";
				dynamic viewBag = new ExpandoObject();
				viewBag.MaxLevels = maxLevels;
				viewBag.RenderAllLevels = renderAllLevels;

				var executionResult = InMemoryRazorEngine.Execute(template, model, viewBag, typeof(INode).Assembly, typeof(ExpandoObject).Assembly, typeof(Binder).Assembly);
				return new MvcHtmlString(executionResult.RuntimeResult);
			};

			var htmlHelper = new HtmlHelper(new ViewContext(), new ViewPage());
			menu = htmlHelper.Menu("NamedSitemap", maxLevels: 2);
		};

		Cleanup after = () =>
		{
			NavigationConfiguration.Initialise(null);
			NavigationConfiguration.Initialise("NamedSitemap", null);
		};

		It should_generate_menu =
			() => menu.ToString().ShouldEqual("Title:Action1, maxLevels:2, renderAllLevels:false");
	}
}