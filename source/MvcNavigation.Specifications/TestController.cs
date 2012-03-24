// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Web.Mvc;

namespace MvcNavigation.Specifications
{
	public class TestController : Controller
	{
		public void RootAction()
		{
		}

		public void Action1()
		{
		}

		public void Action2()
		{
		}

		public void Action3()
		{
		}

		public void Action4()
		{
		}

		[ActionName("Action")]
		public void DecoratedAction()
		{
		}

		public void ParameterisedAction(int param1, string param2)
		{
		}
	}
}