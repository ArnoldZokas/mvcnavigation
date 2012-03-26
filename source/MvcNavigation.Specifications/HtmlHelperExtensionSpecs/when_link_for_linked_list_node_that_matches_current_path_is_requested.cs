// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Collections.Generic;
using System.Web.Mvc;
using Machine.Specifications;

namespace MvcNavigation.Specifications.HtmlHelperExtensionSpecs
{
	[Subject(typeof(HtmlHelperExtensions))]
	public class when_link_for_linked_list_node_is_requested : action_link_spec
	{
		static MvcHtmlString link;

		Because of = () =>
		{
			view_context.RouteData.Values.Add("controller", "Test");
			view_context.RouteData.Values.Add("action", "Action1");

			route_collection.MapRoute("Test", "action1", new { controller = "Test", action = "Action1" });

			link = html_helper.ActionLink(new LinkedListNode<INode>(new Node<TestController>(c => c.Action1())));
		};

		It should_generate_link =
			() => link.ToString().ShouldEqual("<a href=\"/action1\">Action1</a>");
	}
}