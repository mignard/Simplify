using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.XPath;
using Simplify.Extensions.Xml;

namespace Simplify.Web.Mvc
{
	/// <summary>
	/// Localizable view
	/// </summary>
	public class LocalizableView : IView
	{
		public static readonly string LayoutLocalizationPath = "~/Views/Shared/_Layout.cshtml";

		private static IFileSystem FileSystemInstance = new FileSystem();
		public static IFileSystem FileSystem
		{
			get { return FileSystemInstance; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				FileSystemInstance = value;
			}
		}

		private readonly IView _view;
		private readonly string _viewPath;
		private readonly bool _loadLayoutLocalization;
		private readonly string _language;
		private readonly string _defaultLanguage;

		public LocalizableView(IView view, string viewPath, bool loadLayoutLocalization, string language, string defaultLanguage = "en")
		{
			_view = view;
			_viewPath = viewPath;
			_loadLayoutLocalization = loadLayoutLocalization;
			_language = language;
			_defaultLanguage = defaultLanguage;
		}

		/// <summary>
		/// Renders the specified view context by using the specified the writer object.
		/// </summary>
		/// <param name="viewContext">Information related to rendering a view, such as view data, temporary data, and form context.</param>
		/// <param name="writer">The writer object.</param>
		public void Render(ViewContext viewContext, TextWriter writer)
		{
			var stringTable = LoadStringTable(viewContext.HttpContext.Request.MapPath(_viewPath), _language, _defaultLanguage);

			foreach (var item in stringTable)
				viewContext.ViewData.Add(item);

			// Loading layout localization

			if (_loadLayoutLocalization)
			{
				stringTable = LoadStringTable(viewContext.HttpContext.Request.MapPath(LayoutLocalizationPath), _language, _defaultLanguage);

				foreach (var item in stringTable)
					viewContext.ViewData.Add(item);
			}

			_view.Render(viewContext, writer);
		}

		private static IDictionary<string, object> LoadStringTable(string viewPhysicalPath, string language, string defaultLanguage = "en")
		{
			return BuildStringTable(LoadLocalizationFile(viewPhysicalPath, language), LoadLocalizationFile(viewPhysicalPath, defaultLanguage));
		}

		private static XDocument LoadLocalizationFile(string viewPhysicalPath, string language)
		{
			var stringTableFileName = string.Format("{0}.{1}.xml", viewPhysicalPath, language);

			var fileData = FileSystem.File.Exists(stringTableFileName)
				? FileSystem.File.ReadAllText(stringTableFileName)
				: null;

			return !string.IsNullOrEmpty(fileData) ? XDocument.Parse(fileData) : null;
		}

		private static IDictionary<string, object> BuildStringTable(XDocument currentCultureStringTable, XDocument defaultCultureStringTable)
		{
			var stringTable = new Dictionary<string, object>();

			if (currentCultureStringTable != null && currentCultureStringTable.Root != null)

				foreach (var item in currentCultureStringTable.Root.XPathSelectElements("item").Where(x => x.HasAttributes))
					stringTable.Add((string)item.Attribute("name"),
						string.IsNullOrEmpty(item.Value) ? (string)item.Attribute("value") : item.InnerXml().Trim());

			if (defaultCultureStringTable == null || defaultCultureStringTable.Root == null)
				return stringTable;

			foreach (var item in defaultCultureStringTable.Root.XPathSelectElements("item").Where(x => x.HasAttributes))
				if (!stringTable.ContainsKey((string)item.Attribute("name")))
					stringTable.Add((string)item.Attribute("name"),
						string.IsNullOrEmpty(item.Value) ? (string)item.Attribute("value") : item.InnerXml().Trim());

			return stringTable;
		}
	}
}