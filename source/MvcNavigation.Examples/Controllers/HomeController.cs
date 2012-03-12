﻿// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Web.Mvc;

namespace MvcNavigation.Examples.Controllers
{
	public class HomeController : Controller
	{
		[ActionName("Home")]
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Contact()
		{
			return View();
		}
	}
}