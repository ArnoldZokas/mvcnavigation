using System.Web.Mvc;

namespace MvcNavigation.IntegrationTests.Controllers
{
	public class ProductController : Controller
	{
		[ActionName("Shop")]
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Category(int id)
		{
			ViewBag.Id = id;

			return View();
		}
	}
}