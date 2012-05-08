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
		}
	}
}