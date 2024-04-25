using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace siwar.Infrastructure.Mail.Configuration
{
    public class MailConfigureOptions : IConfigureOptions<MailOptions>
    {
        private readonly IConfiguration _configuration;

        public MailConfigureOptions(IConfiguration configuration) =>
            _configuration = configuration;

        public void Configure(MailOptions options) =>
            _configuration.GetSection("Mail").Bind(options);
    }
}
