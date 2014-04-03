using System.Web.Mvc;

namespace Simplify.Web.Mvc
{
	/// <summary>
	/// Localizable razor views engine
	/// </summary>
	public class LocalizableRazorViewEngine : RazorViewEngine
	{
		/// <summary>
		/// Creates a view by using the specified controller context and the paths of the view and master view.
		/// </summary>
		/// <param name="controllerContext">The controller context.</param>
		/// <param name="viewPath">The path to the view.</param>
		/// <param name="masterPath">The path to the master view.</param>
		/// <returns>
		/// The view.
		/// </returns>
		protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
		{
			var view = new LocalizableRazorView(controllerContext, viewPath,
				masterPath, true, FileExtensions, ViewPageActivator);

			return view;
		}
	}
}