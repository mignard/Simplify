using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;

namespace Simplify.Web.Mvc.Tests
{
	[TestFixture]
	public class LocalizableRazorViewTests
	{
		private IFileSystem _fileSystem;

		[SetUp]
		public void SetupFileSystem()
		{
			var files = new Dictionary<string, MockFileData>();

			files.Add("Views/Home/Test.cshtml.en.xml",
				"<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Your site title!\" /><item name=\"Var1\" value=\"Hello world!!!\" /></items>");
			files.Add("Views/Home/Test.cshtml.ru.xml",
				"<?xml version=\"1.0\" encoding=\"utf-8\" ?><items><item name=\"SiteTitle\" value=\"Заголовок сайта!\" /></items>");

			_fileSystem = new MockFileSystem(files, "C:/WebSites/FooSite");
		}

		[Test]
		public void CreateAndRender_SingleLocalizationFile_DataAddedToViewData()
		{
			//var view = new LocalizableRazorView(context.Object, , "", false, new List<string>{"cshtml", "vbhtml"});

			// Arrange

			LocalizableView.FileSystem = _fileSystem;
			var viewContext = new Mock<ViewContext>();
			var textWriter = new Mock<TextWriter>();
			var baseView = new Mock<IView>();

			viewContext.Setup(x => x.HttpContext.Request.MapPath(It.IsAny<string>())).Returns("C:/WebSites/FooSite/Views/Home/Test.cshtml");
			viewContext.SetupProperty(x => x.ViewData, new ViewDataDictionary());

			// Act

			var view = new LocalizableView(baseView.Object, "~/Views/Home/Test.cshtml", "ru");

			view.Render(viewContext.Object, textWriter.Object);

			// Assert

			Assert.AreEqual(2, viewContext.Object.ViewData.Count);
			Assert.AreEqual("Заголовок сайта!", viewContext.Object.ViewData["SiteTitle"]);
			Assert.AreEqual("Hello world!!!", viewContext.Object.ViewData["Var1"]);
			Assert.IsNull(viewContext.Object.ViewData["Var2"]);
		}
	}
}