using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OeuilDeSauron.Domain.Models
{
    public class HealthCheckRequest
    {
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int ResponseTime { get; set; }
        public string Url { get; set; }
        public IDictionary<string, string> Headers { get; set; }
    }
}
