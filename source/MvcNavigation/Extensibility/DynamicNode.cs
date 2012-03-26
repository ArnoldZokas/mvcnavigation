// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MvcNavigation.Extensibility
{
	public abstract class DynamicNode<TController> : NodeBase where TController : IController
	{
		IEnumerable<INode> _childNodes;

		protected DynamicNode(Expression<Action<TController>> action) : this(action, null)
		{
		}

		protected DynamicNode(Expression<Action<TController>> action, string title)
		{
			if (action == null)
				throw new ArgumentNullException("action");

			var methodCallExpression = action.Body as MethodCallExpression;
			if (methodCallExpression == null)
				throw new ArgumentException("Node must be initialised with method call expression (e.g. controller => controller.Action())", "action");

			Initialise(methodCallExpression, title);
			InitialiseChildNodes();
		}

		public override IEnumerable<INode> Children
		{
			get { return _childNodes; }
		}

		public abstract IEnumerable<INode> CreateChildNodes();

		void InitialiseChildNodes()
		{
			var childNodes = CreateChildNodes();

			foreach (var childNode in childNodes)
				childNode.SetParent(this);

			_childNodes = childNodes;
		}
	}

	public abstract class DynamicNode<TController, TAreaRegistration> : DynamicNode<TController> where TController : IController where TAreaRegistration : AreaRegistration
	{
		protected DynamicNode(Expression<Action<TController>> action) : this(action, null)
		{
		}

		protected DynamicNode(Expression<Action<TController>> action, string title) : base(action, title)
		{
			// ReSharper disable DoNotCallOverridableMethodsInConstructor
			AreaName = Activator.CreateInstance<TAreaRegistration>().AreaName;
			SetAreaName(RouteValues, AreaName);
			// ReSharper restore DoNotCallOverridableMethodsInConstructor
		}
	}
}