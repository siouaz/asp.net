using System;
using System.Net.Mail;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

using OeuilDeSauron.Infrastructure.Mail.Configuration;

namespace OeuilDeSauron.Infrastructure.Mail
{
    public class Mailer : IMailer
    {
        private readonly ISendGridClient _sendGrid;
        private readonly IViewRenderer _viewRenderer;
        private readonly MailOptions _options;

        public Mailer(ISendGridClient sendGrid, IViewRenderer viewRenderer, IOptions<MailOptions> options)
        {
            _sendGrid = sendGrid;
            _viewRenderer = viewRenderer;
            _options = options.Value;
        }

        public async Task SendAsync<T>(T model, string view, MailMessage message)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var msg = message.ToSendGridMessage();
            msg.HtmlContent = await _viewRenderer.RenderAsync(view, model);

            await _sendGrid.SendEmailAsync(msg);
        }
    }

    internal static class MailMessageExtensions
    {
        internal static SendGridMessage ToSendGridMessage(this MailMessage message)
        {
            var msg = new SendGridMessage
            {
                Subject = message.Subject,
                From = new EmailAddress(message.From?.Address, message.From?.DisplayName)
            };

            foreach (var to in message.To)
            {
                msg.AddTo(to.Address, to.DisplayName);
            }

            foreach (var cc in message.CC)
            {
                msg.AddCc(cc.Address, cc.DisplayName);
            }

            foreach (var bcc in message.Bcc)
            {
                msg.AddBcc(bcc.Address, bcc.DisplayName);
            }

            return msg;
        }
    }
}
