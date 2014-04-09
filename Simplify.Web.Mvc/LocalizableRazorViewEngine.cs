using System;
using System.Threading;
using System.Web.Mvc;

namespace Simplify.Web.Mvc
{
	/// <summary>
	/// Localizable razor views engine
	/// </summary>
	public class LocalizableRazorViewEngine : RazorViewEngine
	{
		private static ILocalizedViewSettings SettingInstance;

		/// <summary>
		/// LocalizedViewSettings settings
		/// </summary>
		public static ILocalizedViewSettings Settings
		{
			get { return SettingInstance ?? (SettingInstance = new LocalizedViewSettings()); }

			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				SettingInstance = value;
			}
		}

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
			var baseView = new RazorView(controllerContext, viewPath, masterPath, true, FileExtensions, ViewPageActivator);
			var view = new LocalizableView(baseView, viewPath, true, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName, Settings.DefaultLanguage);
			return view;
		}

		protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
		{
			var baseView = new RazorView(controllerContext, partialPath, null, false, FileExtensions, ViewPageActivator);
			var view = new LocalizableView(baseView, partialPath, false, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName, Settings.DefaultLanguage);
			return view;
		}
	}
}