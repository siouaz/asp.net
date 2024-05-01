using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OeuilDeSauron.Domain.Interfaces;
using OeuilDeSauron.Domain.Models;
using RestSharp;

namespace OeuilDeSauron.Domain.Services
{
    public class MyHealthCheck : IMyHealthCheck
    {
        public async Task<HealthCheckResponse> CheckHealthAsync(HealthCheckRequest requestParameters)
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
            var healthCheckResponse = new HealthCheckResponse
            {
                Duration = duration,
                Name=requestParameters.Name
            };
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "StatusCode", response.StatusCode.ToString() },
                { "StatusDescription",response.StatusDescription.ToString() },
                { "IsSuccessful",response.IsSuccessful.ToString() },
            };

            if (response.IsSuccessful)
            {
                healthCheckResponse.HealthCheckResult = HealthCheckResult.Healthy("API Healthy , Up and Running", data);
            }
            else
            {
            healthCheckResponse.HealthCheckResult = HealthCheckResult.Unhealthy("API Unhealthy , Something went wrong .. see data for more details ..", response.ErrorException, data);
            }

            return healthCheckResponse;
        }
       
    }
}
