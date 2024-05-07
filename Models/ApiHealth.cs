using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using OeuilDeSauron.Models;

namespace Models
{
    public class ApiHealth
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ProjectId { get; set; }
        public Project Project { get; set; }
        public string ProjectName { get; set; }
        public TimeSpan Duration { get; set; }
        public string DateTime {  get; set; }

        // Serialized version of HealthCheckResult
        public string HealthCheckResultData { get; set; }

        // NotMapped property to handle the HealthCheckResult complex type or enum
        [NotMapped]
        public HealthCheckResult HealthCheckResult
        {
            get => JsonConvert.DeserializeObject<HealthCheckResult>(HealthCheckResultData);
            set => HealthCheckResultData = JsonConvert.SerializeObject(value);
        }
    }
}
