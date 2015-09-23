using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MvcNavigation
{
	public class Node<TController> : NodeBase where TController : IController
	{
		IList<INode> _childNodes;

        public Node(Expression<Action<TController>> action)
            : this(action, null, String.Empty, new INode[0])
		{
		}

		public Node(Expression<Action<TController>> action, string title) : this(action, title, String.Empty, new INode[0])
		{
		}

        public Node(Expression<Action<TController>> action, params INode[] childNodes)
            : this(action, null, String.Empty, childNodes)
		{
		}

        public Node(Expression<Action<TController>> action, string title, string iconFileName, params INode[] childNodes)
		{
			if (action == null)
				throw new ArgumentNullException("action");

			if (childNodes == null)
				throw new ArgumentNullException("childNodes");

			var methodCallExpression = action.Body as MethodCallExpression;
			if (methodCallExpression == null)
				throw new ArgumentException("Node must be initialised with method call expression (e.g. controller => controller.Action())", "action");

			Initialise(methodCallExpression, title, iconFileName);
			InitialiseChildNodes(childNodes);
		}

		public override IEnumerable<INode> Children
		{
			get { return _childNodes; }
		}

		void InitialiseChildNodes(INode[] childNodes)
		{
			foreach (var childNode in childNodes)
				childNode.SetParent(this);

			_childNodes = new List<INode>(childNodes);
		}
	}

	public class Node<TController, TAreaRegistration> : Node<TController> where TController : IController where TAreaRegistration : AreaRegistration
	{
		public Node(Expression<Action<TController>> action) : this(action, null, new INode[0])
		{
		}

		public Node(Expression<Action<TController>> action, string title) : this(action, title, new INode[0])
		{
		}

        public Node(Expression<Action<TController>> action, string title, string iconFileName)
            : this(action, title, iconFileName, new INode[0])
        {
        }

		public Node(Expression<Action<TController>> action, params INode[] childNodes) : this(action, null, childNodes)
		{
		}

		public Node(Expression<Action<TController>> action, string title,  params INode[] childNodes) : base(action, title, String.Empty, childNodes)
		{
			// ReSharper disable DoNotCallOverridableMethodsInConstructor
			AreaName = Activator.CreateInstance<TAreaRegistration>().AreaName;
			SetAreaName(RouteValues, AreaName);
			// ReSharper restore DoNotCallOverridableMethodsInConstructor
		}

        public Node(Expression<Action<TController>> action, string title, string iconFileName, params INode[] childNodes)
            : base(action, title, iconFileName, childNodes)
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            AreaName = Activator.CreateInstance<TAreaRegistration>().AreaName;
            SetAreaName(RouteValues, AreaName);
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }
	}
}
