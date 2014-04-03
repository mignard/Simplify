using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace Simplify.Web.Mvc
{
	/// <summary>
	/// Localizable razor engine view
	/// </summary>
	public class LocalizableRazorView : RazorView
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LocalizableRazorView"/> class.
		/// </summary>
		/// <param name="controllerContext">The controller context.</param>
		/// <param name="viewPath">The view path.</param>
		/// <param name="layoutPath">The layout or master page.</param>
		/// <param name="runViewStartPages">A value that indicates whether view start files should be executed before the view.</param>
		/// <param name="viewStartFileExtensions">The set of extensions that will be used when looking up view start files.</param>
		public LocalizableRazorView(ControllerContext controllerContext, string viewPath, string layoutPath, bool runViewStartPages, IEnumerable<string> viewStartFileExtensions) : base(controllerContext, viewPath, layoutPath, runViewStartPages, viewStartFileExtensions)
		{
			LoadLocalizationFiles();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LocalizableRazorView"/> class.
		/// </summary>
		/// <param name="controllerContext">The controller context.</param>
		/// <param name="viewPath">The view path.</param>
		/// <param name="layoutPath">The layout or master page.</param>
		/// <param name="runViewStartPages">A value that indicates whether view start files should be executed before the view.</param>
		/// <param name="viewStartFileExtensions">The set of extensions that will be used when looking up view start files.</param>
		/// <param name="viewPageActivator">The view page activator.</param>
		public LocalizableRazorView(ControllerContext controllerContext, string viewPath, string layoutPath, bool runViewStartPages, IEnumerable<string> viewStartFileExtensions, IViewPageActivator viewPageActivator)
			: base(controllerContext, viewPath, layoutPath, runViewStartPages, viewStartFileExtensions, viewPageActivator)
		{
			LoadLocalizationFiles();
		}

		private void LoadLocalizationFiles()
		{		
		}

		/// <summary>
		/// Renders the specified view context by using the specified the writer object.
		/// </summary>
		/// <param name="viewContext">Information related to rendering a view, such as view data, temporary data, and form context.</param>
		/// <param name="writer">The writer object.</param>
		public override void Render(ViewContext viewContext, TextWriter writer)
		{
			base.Render(viewContext, writer);
		}		 
	}
}