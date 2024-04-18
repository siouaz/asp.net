using FluentValidation;
using Microsoft.Extensions.Localization;

using siwar.Domain.Identity;

namespace siwar.Domain.Validators;

/// <summary>
/// <see cref="ResetPassword"/> validator.
/// </summary>
public class ResetPasswordValidator : ResetPasswordBaseValidator<ResetPassword>
{
    public ResetPasswordValidator(IStringLocalizer<Resources> localizer) : base(localizer)
    {
        RuleFor(x => x.Password).NotEmpty().WithMessage(localizer.GetString("ResetPasswordPasswordRequired"));
        RuleFor(x => x.Password).Length(8, 64).WithMessage(localizer.GetString("ResetPasswordPasswordLength"));
        RuleFor(x => x.PasswordConfirm).Equal(x => x.Password).WithMessage(localizer.GetString("ResetPasswordPasswordConfirmMatch"));
    }
}

/// <summary>
/// <see cref="ResetPasswordBase"/> validator.
/// </summary>
public class ResetPasswordBaseValidator<T> : AbstractValidator<T> where T : ResetPasswordBase
{
    public ResetPasswordBaseValidator(IStringLocalizer<Resources> localizer)
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage(localizer.GetString("ResetPasswordIdRequired"));
        RuleFor(x => x.Token).NotEmpty().WithMessage(localizer.GetString("ResetPasswordTokenRequired"));
    }
}
