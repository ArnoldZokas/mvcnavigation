// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Collections.ObjectModel;
using System.Reflection;

namespace MvcNavigation
{
	public interface INode
	{
		MethodInfo ActionInfo { get; }
		ReadOnlyCollection<INode> Children { get; }
	}
}