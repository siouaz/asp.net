using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using FluentValidation;

using siwar.Data.Pagination;
using siwar.Domain.Queries;
using siwar.Domain.Models.Identity;
using siwar.Domain.Commands.Identity;
using siwar.Domain.Services;
using siwar.Domain;

namespace siwar.Controllers.Identity;

/// <summary>
/// User controller.
/// </summary>
[Authorize(Policy = "UserPolicy")]
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IMediator _mediator;
    private readonly IRoleQueries _roleQueries;
    private readonly IUserQueries _userQueries;
    private readonly IUserService _userService;

    public UserController(
        ILogger<UserController> logger,
        IMediator mediator,
        IRoleQueries roleQueries,
        IUserQueries userQueries,
        IUserService userService)
    {
        _logger = logger;
        _mediator = mediator;
        _roleQueries = roleQueries;
        _userQueries = userQueries;
        _userService = userService;
    }

    [Authorize(Roles =
        $"{Roles.GlobalAdministratorRole}, {Roles.ValidInspectionOrganismManager}, {Roles.ValidTrainingCenterManager}")]
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserModel user, CancellationToken cancellationToken)
    {
        try
        {
            var createUserCommand = new CreateUserCommand(user);
            var createdUserId = await _mediator.Send(createUserCommand, cancellationToken);
            return Ok();
        }
        catch (ValidationException e)
        {
            var firstErrorMessage = e.Errors.First().ErrorMessage;
            _logger.LogError(firstErrorMessage);
            return BadRequest($"L'utilisateur n'a pas pu être créé : {firstErrorMessage}");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest($"L'utilisateur n'a pas pu être créé : {e.Message}");
        }
    }

    [Authorize(Roles = Roles.GlobalAdministratorRole)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteUserCommand(id), cancellationToken);
            return Ok();
        }
        catch (ValidationException e)
        {
            var firstErrorMessage = e.Errors.First().ErrorMessage;
            _logger.LogError(firstErrorMessage);
            return BadRequest($"Un problème est survenu : {firstErrorMessage}");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest($"Un problème est survenu : {e.Message}");
        }
    }

    [AllowAnonymous]
    [HttpGet("~/api/user")]
    public async Task<IActionResult> GetCurrentAsync() =>
        Ok(await _userService.GetCurrentUserAsync());

    [AllowAnonymous]
    [HttpGet("~/api/user/profile")]
    public async Task<IActionResult> GetProfile()
    {
        try
        {
            var profile = await _userQueries.GetProfileAsync();
            return Ok(profile);
        }
        catch (Exception e)
        {
            return BadRequest($"Un problème est survenu : {e.Message}");
        }
    }

    [AllowAnonymous]
    [HttpPut("~/api/user/profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] ProfileModel profile,
        CancellationToken cancellationToken)
    {
        try
        {
            var updateProfileCommand = new UpdateProfileCommand(profile);
            await _mediator.Send(updateProfileCommand, cancellationToken);
            return Ok();
        }
        catch (ValidationException e)
        {
            var firstErrorMessage = e.Errors.First().ErrorMessage;
            _logger.LogError(firstErrorMessage);
            return BadRequest($"Le profil n'a pas pu être mis à jour : {firstErrorMessage}");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest($"Le profil n'a pas pu être mis à jour : {e.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userQueries.GetByIdForAdminInterfaceAsync(id, cancellationToken);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest($"Un problème est survenu : {e.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] FilterPaginationOptions options,
        CancellationToken cancellationToken)
    {
        var users = await _userQueries.GetAllAsync(options, cancellationToken);
        return Ok(users);
    }

    [Authorize(Roles =
        $"{Roles.GlobalAdministratorRole}, {Roles.ValidInspectionOrganismManager}, {Roles.ValidTrainingCenterManager}")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UserModel user,
        CancellationToken cancellationToken)
    {
        try
        {
            var updateUserCommand = new UpdateUserCommand(id, user);
            await _mediator.Send(updateUserCommand, cancellationToken);
            return Ok();
        }
        catch (ValidationException e)
        {
            var firstErrorMessage = e.Errors.First().ErrorMessage;
            _logger.LogError(firstErrorMessage);
            return BadRequest($"L'utilisateur n'a pas pu être mis à jour : {firstErrorMessage}");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest($"L'utilisateur n'a pas pu être mis à jour : {e.Message}");
        }
    }
}
