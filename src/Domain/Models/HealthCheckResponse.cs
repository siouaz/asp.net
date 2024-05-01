using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OeuilDeSauron.Domain.Models
{
    public class HealthCheckResponse
    {
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public HealthCheckResult HealthCheckResult { get; set; }
    }
}
