using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace siwar.Domain.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string HealthcheckUrl { get; set; }

        [Required]
        public string SiteUrl { get; set; }

        [Required]
        public int CheckFrequency { get; set; }

        public int AssignedTo { get; set; }

        public bool IsActive { get; set; }
    }
}
