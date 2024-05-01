using System;

namespace OeuilDeSauron.Domain.Models.Identity
{
    public class ProfileModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public bool Active { get; set; }
    }
}
