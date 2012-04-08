// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using MvcNavigation.Extensibility;

namespace MvcNavigation.Specifications.ExtensibilitySpecs.DynamicNodeSpecs
{
	public sealed class AreaAwareDynamicNodeImpl<TController, TAreaRegistration> : DynamicNode<TController, TAreaRegistration> where TController : IController where TAreaRegistration : AreaRegistration
	{
		public AreaAwareDynamicNodeImpl(Expression<Action<TController>> action) : base(action)
		{
		}

		public AreaAwareDynamicNodeImpl(Expression<Action<TController>> action, string title) : base(action, title)
		{
		}

		public override IList<INode> CreateChildNodes()
		{
			return new List<INode>();
		}
	}
}