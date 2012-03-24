// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcNavigation
{
	public abstract class NodeBase : INode
	{
		protected internal MethodInfo ActionInfo { get; set; }

		#region INode Members

		public virtual string Title { get; protected set; }
		public virtual string ActionName { get; protected set; }
		public virtual string ControllerName { get; protected set; }
		public abstract ReadOnlyCollection<INode> Children { get; }
		public virtual RouteValueDictionary Arguments { get; protected set; }

		#endregion

		protected void Initialise(MethodCallExpression methodCallExpression, string title)
		{
			ActionInfo = methodCallExpression.Method;

			var parameterInfo = new RouteValueDictionary();
			var parameters = ActionInfo.GetParameters();
			var arguments = methodCallExpression.Arguments;

			for (int i = 0; i < parameters.Length; i++)
			{
				var parameter = parameters[i];
				var argument = arguments[i];

				if (argument.NodeType == ExpressionType.Constant)
					parameterInfo.Add(parameter.Name, ((ConstantExpression)argument).Value);
			}

			Arguments = parameterInfo;

			var actionNameAttribute = ActionInfo.GetCustomAttributes(typeof(ActionNameAttribute), inherit: true).Cast<ActionNameAttribute>().SingleOrDefault();
			var actionName = actionNameAttribute != null ? actionNameAttribute.Name : ActionInfo.Name;

			Title = title ?? actionName;
			ActionName = actionName;

			ControllerName = ActionInfo.DeclaringType.Name.Replace("Controller", string.Empty);
		}
	}
}