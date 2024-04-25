using System;

namespace siwar.Domain.Models.Identity
{
    public class RoleModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Optional property
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Optional property
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
