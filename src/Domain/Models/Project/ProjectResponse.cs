using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OeuilDeSauron.Domain.Models.Project
{
    public class ProjectResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string HealthcheckUrl { get; set; }
        public string SiteUrl { get; set; }
        public string AssignedTo { get; set; }
        public bool IsActive { get; set; }
        public int DurationInMinute { get; set; }
        public int MaxResponseTimeInSecond { get; set; }
        public string HeadersSerialized { get; set; }
        public Dictionary<string, string> Headers { get; set; }
    }
}
