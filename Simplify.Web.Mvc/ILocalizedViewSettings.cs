namespace Simplify.Web.Mvc
{
	/// <summary>
	/// Localized view settings interface
	/// </summary>
	public interface ILocalizedViewSettings
	{
		/// <summary>
		/// Default language, for example: "en", "ru", "de" etc., default value is "en"
		/// </summary>
		string DefaultLanguage { get; }
	}
}