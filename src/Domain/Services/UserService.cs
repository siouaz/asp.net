using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using siwar.Data;
using siwar.Data.Identity;
using siwar.Data.Infrastructure;
using siwar.Domain.Extensions;
using siwar.Domain.Identity;
using siwar.Domain.Interfaces;
using siwar.Domain.Models.Identity;
using siwar.Domain.Queries;

namespace siwar.Domain.Services;

public class UserService : IUserService
{
    private readonly ICurrentUserService _current;
    private readonly ILogger<UserService> _logger;
    private readonly IUserLoginRepository _loginRepository;
    private readonly MonitoringContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IUserQueries _userQueries;

    public UserService(
        ICurrentUserService current,
        ILogger<UserService> logger,
        IUserLoginRepository loginRepository,
        MonitoringContext context,
        UserManager<User> userManager,
        IUserQueries userQueries)
    {
        _current = current;
        _logger = logger;
        _loginRepository = loginRepository;
        _context = context;
        _userManager = userManager;
        _userQueries = userQueries;
    }

    /// <inheritdoc />
    public async Task<CurrentUser> GetCurrentUserAsync()
    {
        var currentUser = _current.User;
        if (currentUser.Identity.IsAuthenticated)
        {
            var userRoles = _current.User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
            return new CurrentUser(
                _current.UserId,
                currentUser.Identity?.Name,
                CultureInfo.CurrentCulture.Name,
                userRoles,
                currentUser.IsAdmin(),
                currentUser.Identity.IsAuthenticated);
        }

        return null;
    }

    /// <inheritdoc />
    public async Task<string> GenerateLoginAsync(string prefix, string lastName, string firstName,
        bool prefixIsAgreementNumber = true)
    {
        var found = true;
        var logins = await _loginRepository.Query().AsNoTracking().ToListAsync();
        var prefixLogin = prefixIsAgreementNumber ? prefix.RemoveSpecialCharactersAndSpace()[..4] : prefix;
        var addedLogin =
            $"{prefixLogin}{lastName.RemoveSpecialCharactersAndSpace()}{firstName[..1]}"
                .ToUpper();
        var i = 1;
        while (i <= firstName.Length - 1 && found)
        {
            if (logins.Any(l => l.ProviderKey.Equals(addedLogin, StringComparison.OrdinalIgnoreCase)))
            {
                addedLogin =
                    $"{prefixLogin}{lastName.RemoveSpecialCharactersAndSpace()}{firstName[..(i + 1)]}"
                        .ToUpper();
                i++;
                continue;
            }

            i++;
            found = false;
        }

        return addedLogin;
    }

    public async Task<string> GenerateUserName(string lastName, string firstName)
    {
        var addedUsername = $"{lastName.RemoveSpecialCharactersAndSpace()}{firstName.Substring(0, 1)}".ToUpper();
        var found = true;
        var i = 1;
        while (i <= firstName.Length - 1 && found)
        {
            if (await _userManager.FindByNameAsync(addedUsername) is not null)
            {
                addedUsername = $"{lastName.RemoveSpecialCharactersAndSpace()}{firstName.Substring(0, i + 1)}"
                    .ToUpper();
                i++;
                continue;
            }

            i++;
            found = false;
        }

        return addedUsername;
    }

    /// <inheritdoc />
    public async Task UpdateProfileAsync(string userId, ProfileModel requestProfile,
        CancellationToken cancellationToken = default)
    {
        var authenticatedUser = await _userManager.FindByIdAsync(_current.UserId);
        if (authenticatedUser is null || authenticatedUser.IsLockedOut)
        {
            _logger.LogError(
                $"User [{_current.UserId}] Failed to update the profile of [{userId}] because [{_current.UserId}] does not exists or is locked out.");
            throw new UnauthorizedAccessException();
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            _logger.LogError(
                $"User [{authenticatedUser.Id}] failed to update user profile because [{userId}] does not exist");
            throw new ArgumentException("L'utilisateur n'existe pas");
        }

        // Check email
        if (user.Email is null || !user.Email.Equals(requestProfile.Email, StringComparison.OrdinalIgnoreCase))
        {
            var emailExists = await _userManager.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => !x.Id.Equals(user.Id) && x.Email.Equals(requestProfile.Email),
                    cancellationToken);
            if (emailExists is not null)
            {
                _logger.LogError(
                    $"User [{user.Id}] failed to update its profile because the email [{requestProfile.Email}] already exists, current email is [{user.Email}]");
                throw new ArgumentException($"[{requestProfile.Email}] est déjà pris");
            }
        }

        user.SetProfile(requestProfile.FirstName, requestProfile.LastName, requestProfile.Email, user.UserName,
            requestProfile.BirthDate, requestProfile.BirthPlace, requestProfile.Telephone);

        var result = await _userManager.UpdateAsync(user);

        // If current password not null, user wants to change it.
        if (result.Succeeded && !string.IsNullOrWhiteSpace(requestProfile.CurrentPassword))
        {
            // update password
            result = await _userManager.ChangePasswordAsync(user, requestProfile.CurrentPassword,
                requestProfile.Password);
        }

        if (!result.Succeeded)
        {
            _logger.LogError(
                $"User [{user.Id}] failed to update its profile : [{string.Join(',', result.Errors.Select(x => x.Description))}]");
            throw new Exception(result.Errors.First().Description);
        }
    }

    /// <inheritdoc />
    public async Task UpdateUserAsync(string userId, UserModel requestUser,
        CancellationToken cancellationToken = default)
    {
        var authenticatedUser = await _userManager.FindByIdAsync(_current.UserId);
        if (authenticatedUser.IsLockedOut)
        {
            _logger.LogError(
                $"User [{_current.UserId}] Failed to update the profile of [{userId}] because [{_current.UserId}] is locked out.");
            throw new UnauthorizedAccessException("Utilisateur désactivé.");
        }

        // TODO : refactor to a method with clear intention.
        // Get eligible user null if not exist or not eligible.
        var user = await _userQueries.GetByIdForAdminInterfaceEntityAsync(requestUser.Id, cancellationToken);

        // If not eligible or not exists
        if (user is null)
        {
            _logger.LogError(
                $"User [{authenticatedUser.Id}] failed to update user profile because [{userId}] does not exist");
            throw new ArgumentException("L'utilisateur n'existe pas");
        }

        // side effects (either call theses in commands or keep here).

        // Only admin can :
        // - activate/deactivate user
        // - update user global role
        // - update user profile
        if (_current.User.IsAdmin())
        {
            // If something changed.
            if (user.IsLockedOut == requestUser.Active)
            {
                if (requestUser.Active)
                {
                    user.Unlock();
                }
                else
                {
                    user.Lock();
                }

                // Clear too much include
                user.SetRoles(new List<UserRole>());

                // Track
                _context.Entry(user).State = EntityState.Modified;

                var result = await _userManager.UpdateAsync(user);

                // If first activation
                if (result.Succeeded)
                {
                    if (result.Succeeded)
                    {
                        _logger.LogInformation(
                            "User [{Creator}] activated [{NewUserId}] for the first time with email [{NewUserEmail}", authenticatedUser.Id, user.Id, user.Email);
                    }
                }
            }

            await UpdateUserRolesAsync(user.Id, requestUser.Roles, cancellationToken);
            await UpdateProfileAsync(user.Id, requestUser.Profile, cancellationToken);
        }
    }

    /// <inheritdoc />
    public async Task UpdateUserRolesAsync(string userId, IList<string> requestRoleIds,
        CancellationToken cancellationToken)
    {
        var authenticatedUser = await _userManager.FindByIdAsync(_current.UserId);

        if (authenticatedUser.IsLockedOut)
        {
            _logger.LogError(
                $"User [{authenticatedUser.Id}] failed to update user roles because [{authenticatedUser.Id}] is locked out.");
            throw new UnauthorizedAccessException("Utilisateur désactivé");
        }

        // Only admin can update global roles.
        if (!_current.User.IsAdmin())
        {
            _logger.LogError(
                $"User [{authenticatedUser.Id}] failed to update user roles because [{authenticatedUser.Id}] is not an administrator.");
            throw new ArgumentException("Droits insuffisants pour manipuler certaines responsabilités.");
        }

        var user = await _userQueries.GetByIdEntityAsync(userId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(
                $"User [{authenticatedUser.Id}] failed to update user roles because [{userId}] does not exist.");
            throw new ArgumentException("L'utilisateur n'existe pas");
        }

        var requestGlobalRoleIds =
            requestRoleIds.Where(x => Global.Roles.Any(r => r.Equals(x))).Distinct().ToList();
        var globalRoleIdsToAdd = requestGlobalRoleIds.Except(user.Roles.Select(x => x.RoleId)).ToList();
        // Global roles which are not in user roles nor request roles.
        var globalRoleIdsToDelete = user.Roles.Where(x =>
                Global.Roles.Any(r => r.Equals(x.RoleId)) &&
                requestGlobalRoleIds.All(r => !r.Equals(x.RoleId)))
            .Select(x => x.RoleId).Distinct().ToList();

        // If nothing to update.
        if (!globalRoleIdsToAdd.Any() && !globalRoleIdsToDelete.Any())
        {
            return;
        }

        IdentityResult result = null;
        if (globalRoleIdsToAdd.Any())
        {
            // Bad fix to bad problem (userqueries too much include).
            result = await _userManager.AddToRolesAsync(new User { Id = user.Id }, globalRoleIdsToAdd);
        }

        if ((result is null or { Succeeded: true }) && globalRoleIdsToDelete.Any())
        {
            // Bad fix to bad problem (userqueries too much include).
            result = await _userManager.RemoveFromRolesAsync(new User { Id = user.Id }, globalRoleIdsToDelete);
        }

        if (result is { Succeeded: true })
        {
            _logger.LogInformation(
                $"User [{authenticatedUser.Id}] successfully updated user global roles [{user.Id}], [previous state : {string.Join(',', user.Roles.Select(x => x.RoleId))}] -> [current state: {string.Join(',', requestRoleIds)}]");
        }
    }
}
