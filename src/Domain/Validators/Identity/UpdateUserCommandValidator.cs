using Microsoft.Extensions.Localization;

using FluentValidation;

using OeuilDeSauron.Domain.Commands.Identity;

namespace OeuilDeSauron.Domain.Validators;

/// <summary>
/// <see cref="UpdateUserCommandValidator"/> validator.
/// </summary>
public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(IStringLocalizer<Resources> localizer)
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
