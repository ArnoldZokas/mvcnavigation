// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System.Web.Mvc;

namespace MvcNavigation.Internal
{
	internal class ViewDataContainer<TModel> : IViewDataContainer
	{
		public ViewDataContainer(TModel model)
		{
			ViewData = new ViewDataDictionary<TModel>(model);
		}

		#region IViewDataContainer Members

		public ViewDataDictionary ViewData { get; set; }

		#endregion
	}
}