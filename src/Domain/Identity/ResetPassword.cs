namespace OeuilDeSauron.Domain.Identity
{
    /// <summary>
    /// Reset password model.
    /// </summary>
    public class ResetPassword : ResetPasswordBase
    {
        /// <summary>
        /// Gets or sets password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets password confirmation.
        /// </summary>
        public string PasswordConfirm { get; set; }
    }

    /// <summary>
    /// Reset password base model.
    /// </summary>
    public class ResetPasswordBase
    {
        /// <summary>
        /// Gets or sets user id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets reset password token.
        /// </summary>
        public string Token { get; set; }
    }
}
