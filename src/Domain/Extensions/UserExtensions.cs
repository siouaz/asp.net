using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using OeuilDeSauron.Data.Identity;

namespace OeuilDeSauron.Domain.Extensions;

public static class UserExtensions
{
    /// <summary>
    /// Based on User claims principal.
    /// </summary>
    /// <returns>True if user's principal claims contains the admin role.</returns>
    public static bool IsAdmin(this ClaimsPrincipal claims) => claims.IsInRole(Roles.GlobalAdministratorRole);

    /// <summary>
    /// Based on User instance Roles.
    /// </summary>
    /// <returns>True if User.roles contains the admin role.</returns>
    public static bool IsAdmin(this User user) => user.Roles.Any(x =>
        x.RoleId.Equals(Roles.GlobalAdministratorRole, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Based on User claims principal.
    /// </summary>
    /// <returns>User id.</returns>
    public static string GetUserId(this ClaimsPrincipal claims) => claims.FindFirstValue(ClaimTypes.NameIdentifier);
}
