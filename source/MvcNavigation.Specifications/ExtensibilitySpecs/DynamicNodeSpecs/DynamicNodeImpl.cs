using System;
using System.Collections.Generic;
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

		public override IList<INode> CreateChildNodes()
		{
			return new List<INode>
			{
				new Node<TestController>(c => c.Action1()),
				new Node<TestController>(c => c.Action2())
			};
		}
	}
}