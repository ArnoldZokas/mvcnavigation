// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace MvcNavigation
{
	public class Node<TController> : INode where TController : IController
	{
		readonly List<INode> _childNodes;

		public Node(Expression<Action<TController>> action) : this(action, new INode[0])
		{
		}

		public Node(Expression<Action<TController>> action, params INode[] childNodes)
		{
			if (action == null)
				throw new ArgumentNullException("action");

			if (childNodes == null)
				throw new ArgumentNullException("childNodes");

			var methodCallExpression = action.Body as MethodCallExpression;
			if (methodCallExpression == null)
				throw new ArgumentException("Node must be initialised with method call expression (e.g. controller => controller.Action())", "action");

			Initialise(methodCallExpression);
			_childNodes = new List<INode>(childNodes);
		}

		protected internal MethodInfo ActionInfo { get; set; }

		#region INode Members

		public string Title { get; private set; }
		public string ActionName { get; private set; }
		public string ControllerName { get; private set; }

		public ReadOnlyCollection<INode> Children
		{
			get { return new ReadOnlyCollection<INode>(_childNodes); }
		}

		#endregion

		void Initialise(MethodCallExpression methodCallExpression)
		{
			ActionInfo = methodCallExpression.Method;

			var actionNameAttribute = ActionInfo.GetCustomAttributes(typeof(ActionNameAttribute), inherit: true).Cast<ActionNameAttribute>().SingleOrDefault();
			Title = actionNameAttribute != null ? actionNameAttribute.Name : ActionInfo.Name;

			ActionName = ActionInfo.Name;

			ControllerName = ActionInfo.DeclaringType.Name.Replace("Controller", string.Empty);
		}
	}
}