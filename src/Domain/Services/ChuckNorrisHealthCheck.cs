using System;
using System.Collections.Generic;
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
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckRequest requestParameterst)
        {

            var client = new RestClient();

            var request = new RestRequest(requestParameterst.Url, Method.Get);
            foreach(var headerItem in requestParameterst.Headers)
            {
                request.AddHeader(headerItem.Key,headerItem.Value);
            }

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
                return HealthCheckResult.Healthy();

            return HealthCheckResult.Unhealthy();
        }
       
    }
}
