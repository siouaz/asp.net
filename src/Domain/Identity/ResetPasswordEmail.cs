namespace OeuilDeSauron.Domain.Identity
{
    /// <summary>
    /// Reset password email model.
    /// </summary>
    public class ResetPasswordEmail
    {
        /// <summary>
        /// Gets user id.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets user first name.
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets user last name.
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets reset password url.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordEmail"/>
        /// </summary>
        public ResetPasswordEmail(string id, string firstName, string lastName, string url)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Url = url;
        }
    }
}
