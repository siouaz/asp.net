using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OeuilDeSauron.Domain.Models
{
    public class HealthCheckResponse
    {
        public bool IsHealthy { get; set; }
        public object Details { get; set; }
    }
}
