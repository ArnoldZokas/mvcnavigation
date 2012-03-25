// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MvcNavigation.Extensibility
{
	public abstract class DynamicNode<TController> : NodeBase where TController : IController
	{
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