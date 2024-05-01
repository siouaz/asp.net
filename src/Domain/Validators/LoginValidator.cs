using FluentValidation;
using Microsoft.Extensions.Localization;

using OeuilDeSauron.Domain.Identity;

namespace OeuilDeSauron.Domain.Validators;

/// <summary>
/// <see cref="Login"/> validator.
/// </summary>
public class LoginValidator : AbstractValidator<Login>
{
    public LoginValidator(IStringLocalizer<Resources> localizer)
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage(localizer.GetString("LoginUserNameRequired"));
        RuleFor(x => x.Password).NotEmpty().WithMessage(localizer.GetString("LoginPasswordRequired"));
        RuleFor(x => x.Password).Length(8, 64).WithMessage(localizer.GetString("LoginPasswordLength"));
    }
}
