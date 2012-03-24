// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Web.Mvc;
using MvcNavigation.Extensibility;
using MvcNavigation.IntegrationTests.Controllers;

namespace MvcNavigation.IntegrationTests
{
	public class ShopNode<TController> : DynamicNode<TController> where TController : IController
	{
		public ShopNode(Expression<Action<TController>> action) : base(action)
		{
		}

		public ShopNode(Expression<Action<TController>> action, string title) : base(action, title)
		{
		}

		public override ReadOnlyCollection<INode> Children
		{
			get
			{
				var childNodes = new List<INode>
				                 {
				                 	new Node<ProductController>(c => c.Category(1), title: "Category 1 (dynamic)"),
				                 	new Node<ProductController>(c => c.Category(2), title: "Category 2 (dynamic)"),
				                 	new Node<ProductController>(c => c.Category(3), title: "Category 3 (dynamic)")
				                 };


				return new ReadOnlyCollection<INode>(childNodes);
			}
		}
	}
}