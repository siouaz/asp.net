using Microsoft.Extensions.Localization;
using FluentValidation;

using siwar.Domain.Commands.Identity;
using siwar.Domain.Interfaces;

namespace siwar.Domain.Validators;

/// <summary>
/// <see cref="CreateUserCommandValidator"/> validator.
/// </summary>
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(IStringLocalizer<Resources> localizer, ICurrentUserService currentUserService)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        // Profile
        // FirstName should be not empty.
        RuleFor(x => x.User.FirstName).NotEmpty().WithMessage(localizer.GetString("FirstNameRequired"));
        // LastName should be not empty.
        RuleFor(x => x.User.LastName).NotEmpty().WithMessage(localizer.GetString("LastNameRequired"));
        // Email should be not empty.
        RuleFor(x => x.User.Email).NotEmpty().WithMessage(localizer.GetString("EmailRequired"));
        // BirthDate should be not empty.
        RuleFor(x => x.User.BirthDate).NotEmpty().WithMessage(localizer.GetString("BirthDateRequired"));
        // Active should be not null.
        RuleFor(x => x.User.Active).NotNull().WithMessage(localizer.GetString("ActiveRequired"));
    }
}
