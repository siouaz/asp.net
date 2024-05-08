using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Models;
using OeuilDeSauron.Domain.Interfaces;
using OeuilDeSauron.Domain.Models;
using RestSharp;

namespace OeuilDeSauron.Domain.Services
{
    public class MyHealthCheck : IMyHealthCheck
    {
        private readonly IEmailSender _emailSender;
        public MyHealthCheck(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public async Task<ApiHealth> CheckHealthAsync(HealthCheckRequest requestParameters)
        {

            var client = new RestClient();

            var request = new RestRequest(requestParameters.Url, Method.Get);
            foreach(var headerItem in requestParameters.Headers)
            {
                request.AddHeader(headerItem.Key,headerItem.Value);
            }
            var stopwatch = Stopwatch.StartNew();
            var response = await client.ExecuteAsync(request);
            stopwatch.Stop();

            var duration = stopwatch.Elapsed;
            var ApiHealth = new ApiHealth
            {
                Duration = duration,
                ProjectName=requestParameters.ProjectName,
                ProjectId=requestParameters.ProjectId
            };
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "StatusCode", response.StatusCode.ToString() },
                { "StatusDescription",response.StatusDescription.ToString() },
                { "IsSuccessful",response.IsSuccessful.ToString() },
            };

            if (response.IsSuccessful)
            {
                if (duration.TotalSeconds > requestParameters.ResponseTime)
                {
                    ApiHealth.HealthCheckResult = HealthCheckResult.Unhealthy($"Warning ! API Healthy But Response Time Superior than Max Duration {duration.TotalSeconds}",response.ErrorException, data);
                }
                else
                {
                    ApiHealth.HealthCheckResult = HealthCheckResult.Healthy("API Healthy , Up and Running", data);
                }
            }
            else
            {
                ApiHealth.HealthCheckResult = HealthCheckResult.Unhealthy($"API Unhealthy , Something went wrong .. see data for more details .. Duration {duration.TotalSeconds}", response.ErrorException, data);
                var subject = $"Your WebSite {requestParameters.ProjectName} API Is Unhealthy";
                var body = $"Your WebSite API Is Unhealthy , check your account for more details , {response.ErrorMessage}";
                await _emailSender.SendEmailAsync(requestParameters.ProjectMail,subject,body);
            }

            return ApiHealth;
        }
       
    }
}
