using FluentValidation;
using Microsoft.Extensions.Localization;

using OeuilDeSauron.Domain.Identity;

namespace OeuilDeSauron.Domain.Validators;

/// <summary>
/// <see cref="ChangePassword"/> validator.
/// </summary>
public class ChangePasswordValidator : AbstractValidator<ChangePassword>
{
    public ChangePasswordValidator(IStringLocalizer<Resources> localizer)
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage(localizer.GetString("UserIdRequired"));
        RuleFor(x => x.OldPassword).NotEmpty().WithMessage(localizer.GetString("OldPasswordRequired"));
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage(localizer.GetString("NewPasswordRequired"));
        RuleFor(x => x.PasswordConfirm).NotEmpty().WithMessage(localizer.GetString("ConfirmPasswordRequired"));
        RuleFor(x => x.NewPassword).Length(8, 64).WithMessage(localizer.GetString("NewPasswordLength"));
        RuleFor(x => x.NewPassword).Matches("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])").WithMessage(localizer.GetString("NewPasswordPattern"));
    }
}
