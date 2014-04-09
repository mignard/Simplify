using System.Collections.Specialized;
using System.Configuration;

namespace Simplify.Web.Mvc
{
	/// <summary>
	/// Localized view settings
	/// </summary>
	public class LocalizedViewSettings : ILocalizedViewSettings
	{
		/// <summary>
		/// Default language, for example: "en", "ru", "de" etc., default value is "en"
		/// </summary>
		public string DefaultLanguage { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="LocalizedViewSettings"/> class.
		/// </summary>
		/// <param name="configSectionName">Name of the configuration section in the configuration file.</param>
		/// <exception cref="LocalizedViewSettings">
		/// No MailSenderSettings section in config file.
		/// or
		/// MailSenderSettings SmtpServerAddress is empty or missing from config file.
		/// or
		/// MailSenderSettings SmtpUserName is empty or missing from config file.
		/// or
		/// MailSenderSettings SmtpUserPassword is empty or missing from config file.
		/// </exception>
		public LocalizedViewSettings(string configSectionName = "MailSenderSettings")
		{
			DefaultLanguage = "en";

			var config = (NameValueCollection)ConfigurationManager.GetSection(configSectionName);

			if (config == null)
				return;

			if (!string.IsNullOrEmpty(config["DefaultLanguage"]))
				DefaultLanguage = config["DefaultLanguage"];
		}
	}
}