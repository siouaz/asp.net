using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using OeuilDeSauron.Infrastructure.Files;
using OeuilDeSauron.Infrastructure.Mail;
using OeuilDeSauron.Infrastructure.Mail.Configuration;
using SendGrid.Extensions.DependencyInjection;

namespace OeuilDeSauron.Infrastructure
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddFileManager(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton(_ =>
                new BlobServiceClient(configuration.GetConnectionString("Storage")));
            services.TryAddSingleton<IFileManager, FileManager>();

            return services;
        }

        public static IServiceCollection AddMailing(this IServiceCollection services, IHostEnvironment environment)
        {
            services.AddSingleton<IConfigureOptions<MailOptions>, MailConfigureOptions>();

            if (environment.IsDevelopment())
            {
                //services.AddSingleton<IMailer, NoopMailer>();
                services.AddSendGrid(
                    (provider, options) =>
                        options.ApiKey = provider.GetRequiredService<IOptions<MailOptions>>().Value?.ApiKey);
                services.AddScoped<IViewRenderer, ViewRenderer>();
                services.AddScoped<IMailer, Mailer>();
            }
            else
            {
                services.AddSendGrid(
                    (provider, options) =>
                        options.ApiKey = provider.GetRequiredService<IOptions<MailOptions>>().Value?.ApiKey);
                services.AddSingleton<IViewRenderer, ViewRenderer>();
                services.AddSingleton<IMailer, Mailer>();
            }

            return services;
        }
    }
}
