// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcNavigation
{
	public abstract class NodeBase : INode
	{
		protected internal MethodInfo ActionInfo { get; set; }

		#region INode Members

		public virtual string ActionName { get; protected set; }
		public virtual string Title { get; protected set; }
		public virtual string ControllerName { get; protected set; }
		public abstract ReadOnlyCollection<INode> Children { get; }
		public virtual RouteValueDictionary Arguments { get; protected set; }

		#endregion

		protected void Initialise(MethodCallExpression methodCallExpression, string title)
		{
			ActionInfo = methodCallExpression.Method;

			ActionName = GetActionName();
			Title = title ?? ActionName;
			ControllerName = GetControllerName();
			Arguments = GetParameters(methodCallExpression);
		}

		string GetActionName()
		{
			var actionNameAttribute = ActionInfo.GetCustomAttributes(typeof(ActionNameAttribute), inherit: true).Cast<ActionNameAttribute>().SingleOrDefault();
			var actionName = actionNameAttribute != null ? actionNameAttribute.Name : ActionInfo.Name;
			return actionName;
		}

		string GetControllerName()
		{
			// ReSharper disable PossibleNullReferenceException
			return ActionInfo.DeclaringType.Name.Replace("Controller", string.Empty);
			// ReSharper restore PossibleNullReferenceException
		}

		RouteValueDictionary GetParameters(MethodCallExpression methodCallExpression)
		{
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
			return parameterInfo;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendFormat("{0}.{1}()", ActionInfo.DeclaringType.Name, ActionInfo.Name);

			if (ActionName != Title)
				sb.AppendFormat(" \"{0}\"", Title);

			sb.AppendFormat(", child count: {0}", Children.Count);

			return sb.ToString();
		}
	}
}