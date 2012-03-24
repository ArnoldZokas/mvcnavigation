// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Web.Mvc;
using MvcNavigation.Extensibility;

namespace MvcNavigation.Specifications.ExtensibilitySpecs.DynamicNodeSpecs
{
	public sealed class DynamicNodeImpl<TController> : DynamicNode<TController> where TController : IController
	{
		public DynamicNodeImpl(Expression<Action<TController>> action) : base(action)
		{
		}

		public DynamicNodeImpl(Expression<Action<TController>> action, string title) : base(action, title)
		{
		}

		public override ReadOnlyCollection<INode> Children
		{
			get
			{
				var childNodes = new List<INode>
				{
					new Node<TestController>(c => c.Action1()),
					new Node<TestController>(c => c.Action2())
				};
				return new ReadOnlyCollection<INode>(childNodes);
			}
		}
	}
}