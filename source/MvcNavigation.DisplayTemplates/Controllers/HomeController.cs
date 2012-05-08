using System.Web.Mvc;

namespace MvcNavigation.IntegrationTests.Controllers
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

		public ActionResult History()
		{
			return View();
		}

		public ActionResult HistoryPage1()
		{
			return View();
		}

		public ActionResult HistoryPage2()
		{
			return View();
		}

		public ActionResult MoreHistory()
		{
			return View();
		}

		public ActionResult Contact()
		{
			return View();
		}

		public ActionResult ContactPage1()
		{
			return View();
		}

		public ActionResult ContactPage2()
		{
			return View();
		}

		[ChildActionOnly]
		public ActionResult Navigation()
		{
			return View();
		}
	}
}