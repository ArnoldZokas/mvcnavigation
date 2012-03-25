// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Collections.ObjectModel;
using System.Web.Routing;

namespace MvcNavigation
{
	public interface INode
	{
		string ActionName { get; }
		string Title { get; }
		string ControllerName { get; }
		string AreaName { get; }
		RouteValueDictionary RouteValues { get; }
		ReadOnlyCollection<INode> Children { get; }
	}
}