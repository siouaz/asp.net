using System.Net.Mail;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace siwar.Infrastructure.Mail
{
    /// <summary>
    /// Testing mailer.
    /// </summary>
    public class NoopMailer : IMailer
    {
        private readonly ILogger<NoopMailer> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoopMailer"/> class.
        /// </summary>
        public NoopMailer(ILogger<NoopMailer> logger) => _logger = logger;

        /// <inheritdoc/>
        public Task SendAsync<T>(T model, string view, MailMessage message)
        {
            _logger.LogInformation(JsonSerializer.Serialize(model));

            return Task.CompletedTask;
        }
    }
}
