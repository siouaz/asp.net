using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace siwar.Domain.Models.Identity
{
    public class UserModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        /// <summary>
        /// Gets user full name.
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

        public string Email { get; set; }

        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets user phone number.
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Gets or sets user secondary phone number.
        /// </summary>
        public string Telephone2 { get; set; }

        /// <summary>
        /// Gets or sets user ternary phone number.
        /// </summary>
        public string Telephone3 { get; set; }

        /// <summary>
        /// Gets or sets user birth date.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets user birth place.
        /// </summary>
        public string BirthPlace { get; set; }

        /// <summary>
        /// Gets user roles.
        /// </summary>
        public IList<string> Roles { get; set; }

        /// <summary>
        /// Gets user is deletable.
        /// </summary>
        public bool IsDeletable { get; set; }

        public bool HasRunningAssociation { get; set; }

        /// <summary>
        /// Returns profile model except passwords.
        /// </summary>
        [JsonIgnore]
        public ProfileModel Profile =>
            new()
            {
                FirstName = FirstName,
                LastName = LastName,
                BirthDate = BirthDate,
                BirthPlace = BirthPlace,
                Email = Email,
                Telephone = Telephone,
                Active = Active
            };
    }
}
