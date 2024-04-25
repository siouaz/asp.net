using System.Collections.Generic;
using System.Linq;

using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;

namespace siwar.Telemetry
{
    /// <summary>
    /// Excludes configured requests from being logged.
    /// </summary>
    public class ExcludeRequestTelemetryProcessor : ITelemetryProcessor
    {
        private IList<string> Exclude { get; set; }
        
        private ITelemetryProcessor Next { get; set; }

        public ExcludeRequestTelemetryProcessor(IConfiguration configuration, ITelemetryProcessor next)
        {
            Exclude = configuration.GetSection("ApplicationInsights:Request:Exclude").Get<IList<string>>() ?? new List<string>();
            Next = next;
        }

        public void Process(ITelemetry item)
        {
            var request = item as RequestTelemetry;

            if (request is null || Exclude.Any(x => request.Url is not null && request.Url.AbsolutePath.StartsWith(x)))
            {
                return;
            }
            
            Next.Process(item);
        }
    }
}