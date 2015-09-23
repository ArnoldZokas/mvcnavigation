using System.Collections.Generic;
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
        public virtual string IconFileName { get; protected set; }
		public virtual string ControllerName { get; protected set; }
		public virtual string AreaName { get; protected set; }
		public virtual INode Parent { get; protected set; }

		public abstract IEnumerable<INode> Children { get; }

		public void SetParent(INode parent)
		{
			Parent = parent;
		}

		public virtual RouteValueDictionary RouteValues { get; protected set; }

		#endregion

		protected void Initialise(MethodCallExpression methodCallExpression, string title, string iconFileName)
		{
			ActionInfo = methodCallExpression.Method;

			ActionName = GetActionName();
			Title = title ?? ActionName;
            IconFileName = iconFileName;
			ControllerName = GetControllerName();
			AreaName = "";
			RouteValues = GetRouteValues(methodCallExpression);
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

		RouteValueDictionary GetRouteValues(MethodCallExpression methodCallExpression)
		{
			var routeValues = new RouteValueDictionary();
			var parameters = ActionInfo.GetParameters();
			var arguments = methodCallExpression.Arguments;

			for (var i = 0; i < parameters.Length; i++)
			{
				var parameter = parameters[i];
				var argument = arguments[i];

				if (argument.NodeType == ExpressionType.Constant)
					routeValues.Add(parameter.Name, ((ConstantExpression)argument).Value);
			}

			SetAreaName(routeValues, AreaName);

			return routeValues;
		}

		protected void SetAreaName(RouteValueDictionary routeValues, string areaName)
		{
			routeValues["area"] = areaName;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			if (AreaName != "")
				sb.AppendFormat("[{0}]", AreaName);

			// ReSharper disable PossibleNullReferenceException
			sb.AppendFormat("{0}.{1}()", ActionInfo.DeclaringType.Name, ActionInfo.Name);
			// ReSharper restore PossibleNullReferenceException

			if (ActionName != Title)
				sb.AppendFormat(" \"{0}\"", Title);

			sb.AppendFormat(", child count: {0}", Children.Count());

			return sb.ToString();
		}
	}
}
