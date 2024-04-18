namespace siwar.Domain.Models.Identity
{
    public class NewUserAdded
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public NewUserAdded(string firstName, string lastName, string userName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            Password = password;
        }
    }
}

