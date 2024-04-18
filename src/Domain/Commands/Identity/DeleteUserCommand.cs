using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Identity;
using MediatR;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Errors.Model;

using siwar.Data.Identity;
using siwar.Data.Infrastructure;
using siwar.Domain.Extensions;
using siwar.Domain.Interfaces;
using siwar.Domain.Queries;

namespace siwar.Domain.Commands.Identity;

public class DeleteUserCommand : IRequest<IdentityResult>
{
    public string UserId { get; }

    public DeleteUserCommand(string userId)
    {
        UserId = userId;
    }
}

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, IdentityResult>
{
    private readonly ILogger<DeleteUserHandler> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly siwarContext _siwarContext;
    private readonly UserManager<User> _userManager;
    private readonly IUserQueries _userQueries;

    public DeleteUserHandler(ILogger<DeleteUserHandler> logger, IUserQueries userQueries,
        siwarContext siwarContext, UserManager<User> userManager, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _siwarContext = siwarContext;
        _userManager = userManager;
        _userQueries = userQueries;
    }

    public async Task<IdentityResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        // Check if authenticated user is admin
        if (!_currentUserService.User.IsAdmin())
        {
            _logger.LogError(
                $"User [{_currentUserService.UserId}] failed to delete user [{request.UserId}] because he is not admin.");
            throw new UnauthorizedException("Droits insuffisant pour supprimer l'utilisateur.");
        }

        // Check user
        var user = await _userQueries.GetByIdEntityAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            _logger.LogError(
                $"User [{_currentUserService.UserId}] failed to delete user [{request.UserId}] because it does not exists.");
            throw new ArgumentException("L'utilisateur n'existe pas.");
        }

        // Delete related data
        // user roles
        await _siwarContext.UserRoles.Where(x => x.UserId.Equals(user.Id)).BatchDeleteAsync(cancellationToken);
        // user
        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            var firstErrorDescription = result.Errors.First().Description;
            _logger.LogError(
                $"User [{_currentUserService.UserId}] failed to delete [{user.Id}], first error : [{firstErrorDescription}]");
            throw new Exception(result.Errors.First().Description);
        }

        return result;
    }
}
