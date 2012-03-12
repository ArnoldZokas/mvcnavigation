// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

			ActionInfo = methodCallExpression.Method;

			_childNodes = new List<INode>(childNodes);
		}

		protected internal MethodInfo ActionInfo { get; set; }

		#region INode Members

		public string Text
		{
			get { return ActionInfo.Name; }
		}

		public string ActionName
		{
			get { return ActionInfo.Name; }
		}

		public string ControllerName
		{
			get
			{
				// ReSharper disable PossibleNullReferenceException
				return ActionInfo.DeclaringType.Name.Replace("Controller", string.Empty);
				// ReSharper restore PossibleNullReferenceException
			}
		}

		public ReadOnlyCollection<INode> Children
		{
			get { return new ReadOnlyCollection<INode>(_childNodes); }
		}

		#endregion
	}
}