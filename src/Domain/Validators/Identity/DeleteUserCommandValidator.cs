using Microsoft.Extensions.Localization;
using FluentValidation;

using siwar.Domain.Commands.Identity;

namespace siwar.Domain.Validators;

/// <summary>
/// <see cref="CreateUserCommandValidator"/> validator.
/// </summary>
public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator(IStringLocalizer<Resources> localizer)
    {
    }
}
