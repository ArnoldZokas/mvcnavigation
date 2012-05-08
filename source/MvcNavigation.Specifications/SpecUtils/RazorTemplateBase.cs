using System.Text;

namespace MvcNavigation.Specifications.SpecUtils
{
	public abstract class RazorTemplateBase<TModel>
	{
		protected RazorTemplateBase()
		{
			OutputBuilder = new StringBuilder();
		}

		public TModel Model { get; set; }

		public dynamic ViewBag { get; set; }

		public StringBuilder OutputBuilder { get; private set; }

		public abstract void Execute();

		public virtual void Write(object value)
		{
			OutputBuilder.Append(value);
		}

		public virtual void WriteLiteral(object value)
		{
			OutputBuilder.Append(value);
		}
	}
}