// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Collections.ObjectModel;

namespace MvcNavigation
{
	public interface INode
	{
		string Title { get; }
		string ActionName { get; }
		string ControllerName { get; }
		ReadOnlyCollection<INode> Children { get; }
	}
}