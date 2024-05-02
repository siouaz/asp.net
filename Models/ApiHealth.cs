using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OeuilDeSauron.Models;

namespace Models
{
    public class ApiHealth
    {
        public Project Project { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public TimeSpan Duration { get; set; }
        public HealthCheckResult HealthCheckResult { get; set; }
    }
}
