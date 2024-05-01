using System.Collections.Generic;

using OeuilDeSauron.Domain.Models;

namespace OeuilDeSauron.Domain.Identity
{
    /// <summary>
    /// Logged-in user.
    /// </summary>
    public class CurrentUser
    {
        /// <summary>
        /// Gets user id.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets user name.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets user culture.
        /// </summary>
        public string Culture { get; private set; }

        /// <summary>
        /// Gets user roles.
        /// </summary>
        public IList<string> Roles { get; private set; }

        /// <summary>
        /// Gets whether the user is an administrator or not.
        /// </summary>
        public bool? IsAdmin { get; private set; }

        /// <summary>
        /// Gets whether the user is authenticated or not.
        /// </summary>
        public bool IsAuthenticated { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentUser"/> class.
        /// </summary>
        public CurrentUser(string id, string userName, string culture, IList<string> roles, bool isAdmin, bool isAuthenticated = true)
        {
            Id = id;
            UserName = userName;
            Culture = culture;
            Roles = roles;
            IsAdmin = isAdmin;
            IsAuthenticated = isAuthenticated;
        }
    }
}
