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
    public class ProjectRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string HealthcheckUrl { get; set; }

        [Required]
        public string SiteUrl { get; set; }

        [Required]
        [EmailAddress]
        public string AssignedTo { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int DurationInMinute { get; set; }
        [Required]
        public int MaxResponseTimeInSecond { get; set; }
        public string HeadersSerialized { get; set; }
        public Dictionary<string, string> Headers
        {
            get => JsonConvert.DeserializeObject<Dictionary<string, string>>(HeadersSerialized);
            set => HeadersSerialized = JsonConvert.SerializeObject(value);
        }
    }
}
