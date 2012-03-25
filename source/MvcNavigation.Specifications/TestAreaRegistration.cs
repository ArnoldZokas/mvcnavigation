// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using System.Web.Mvc;

namespace MvcNavigation.Specifications
{
	public class TestAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get { return "Test"; }
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			throw new NotImplementedException();
		}
	}
}