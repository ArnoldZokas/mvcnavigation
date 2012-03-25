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
		RouteValueDictionary Arguments { get; }
		ReadOnlyCollection<INode> Children { get; }
	}
}