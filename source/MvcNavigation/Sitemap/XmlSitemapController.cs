// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Web.Mvc;

namespace MvcNavigation.Sitemap
{
	public class XmlSitemapController : Controller
	{
		public ActionResult Sitemap()
		{
			return new XmlSitemapResult(Url);
		}
	}
}