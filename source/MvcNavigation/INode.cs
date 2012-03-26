// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Collections.Generic;
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
		INode Parent { get; }
		IEnumerable<INode> Children { get; }

		void SetParent(INode parent);
	}
}