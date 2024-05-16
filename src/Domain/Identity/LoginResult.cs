using System;

namespace OeuilDeSauron.Domain.Identity
{
    /// <summary>
    /// Login result model.
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// Gets or sets whether the login has succeeded or not.
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Gets login lockout end date, if any.
        /// </summary>
        public DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// Gets whether the user is locked or not.
        /// </summary>
        public bool Locked => LockoutEnd.HasValue && DateTimeOffset.UtcNow < LockoutEnd.Value;

        /// <summary>
        /// Gets result message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginResult"/> class.
        /// </summary>
        public LoginResult(bool success = true) =>
            Success = success;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginResult"/> class.
        /// </summary>
        public LoginResult(bool success, DateTimeOffset? lockoutEnd, string message) : this(success)
        {
            LockoutEnd = lockoutEnd;
            Message = message;
        }
    }
}
