using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using OeuilDeSauron.Domain.Identity;
using OeuilDeSauron.Domain.Models.Identity;

namespace OeuilDeSauron.Domain.Services
{
    public interface IUserService
    {
        Task<CurrentUser> GetCurrentUserAsync();

        Task<string> GenerateUserName(string lastName, string firstName);

        /// <summary>
        /// Updates user profile, certification and structure.
        /// </summary>
        Task UpdateUserAsync(string userId, UserModel requestUser, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates profile, throws if invalid business validation.
        /// </summary>
        Task UpdateProfileAsync(string userId, ProfileModel requestProfile,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates user application/global <see cref="Global.Roles"/>
        /// </summary>
        Task UpdateUserRolesAsync(string userId, IList<string> requestRoleIds,
            CancellationToken cancellationToken);
    }
}
