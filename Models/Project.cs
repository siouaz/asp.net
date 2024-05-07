using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace OeuilDeSauron.Models
{
    public class Project
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        [Required]
        public string HealthcheckUrl { get; set; }

        [Required]
        public string SiteUrl { get; set; }

        [Required]
        public int CheckFrequency { get; set; }

        [Required]
        [EmailAddress]
        public string AssignedTo { get; set; }
        
        public bool IsActive { get; set; }
        public int DurationInMinute { get; set; }

        // Store the serialized dictionary
        public string HeadersSerialized { get; set; }

        [NotMapped]
        public Dictionary<string, string> Headers
        {
            get => JsonConvert.DeserializeObject<Dictionary<string, string>>(HeadersSerialized);
            set => HeadersSerialized = JsonConvert.SerializeObject(value);
        }
    }

}
