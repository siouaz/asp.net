using Microsoft.Extensions.Localization;
using FluentValidation;

using OeuilDeSauron.Domain.Commands.Identity;

namespace OeuilDeSauron.Domain.Validators;

/// <summary>
/// <see cref="CreateUserCommandValidator"/> validator.
/// </summary>
public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator(IStringLocalizer<Resources> localizer)
    {
    }
}
