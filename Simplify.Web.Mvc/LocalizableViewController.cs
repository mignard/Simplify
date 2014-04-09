using System.Web.Mvc;

namespace Simplify.Web.Mvc
{
	public class LocalizableViewController : Controller
	{
		protected override void OnResultExecuting(ResultExecutingContext ctx)
		{
			//this.
			ViewBag.TestA = "123";
			//Server.
			base.OnResultExecuting(ctx);
		}

		protected override ViewResult View(IView view, object model)
		{
			return base.View(view, model);
		}

		protected override ViewResult View(string viewName, string masterName, object model)
		{
			return base.View(viewName, masterName, model);
		}
	}
}