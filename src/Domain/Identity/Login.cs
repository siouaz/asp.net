namespace OeuilDeSauron.Domain.Identity
{
    /// <summary>
    /// Login model.
    /// </summary>
    public class Login
    {
        /// <summary>
        /// Gets or sets user name or email.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets user password.
        /// </summary>
        public string Password { get; set; }
    }
}
