using System;
using System.Web.Mvc;
using System.Web.Routing;
using MvcNavigation.Internal;

namespace MvcNavigation
{
	public static class UrlHelperExtensions
	{
		public static string RouteAbsoluteUrl(this UrlHelper url, INode node)
		{
			if (url == null)
				throw new ArgumentNullException("url");

			if (node == null)
				throw new ArgumentNullException("node");

			var routeValues = new RouteValueDictionary(node.RouteValues);
			routeValues.Add(RouteDataKeys.Action, node.ActionName);
			routeValues.Add(RouteDataKeys.Controller, node.ControllerName);

			var requestUrl = url.RequestContext.HttpContext.Request.Url;
			var relativeUrl = url.RouteUrl(routeValues);

			// ReSharper disable PossibleNullReferenceException
			return string.Format("{0}://{1}{2}", requestUrl.Scheme, requestUrl.Authority, relativeUrl);
			// ReSharper restore PossibleNullReferenceException
		}
	}
}