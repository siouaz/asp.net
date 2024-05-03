using System.Collections.Generic;

namespace OeuilDeSauron.Infrastructure.Mail.Configuration
{
    public class MailOptions
    {
        public string ApiKey { get; set; }

        public Dictionary<string, string> Templates { get; set; }

        public string GetTemplateId(string templateName) =>
            Templates.GetValueOrDefault(templateName);
    }
}
