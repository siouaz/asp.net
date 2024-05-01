using Microsoft.Extensions.Localization;

using FluentValidation;

using OeuilDeSauron.Domain.Commands.Identity;

namespace OeuilDeSauron.Domain.Validators.Identity;

public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileCommandValidator(IStringLocalizer<Resources> localizer)
    {
        // Profile
        RuleFor(x => x.Profile.FirstName).NotEmpty().WithMessage(localizer.GetString("FirstNameRequired"));
        RuleFor(x => x.Profile.LastName).NotEmpty().WithMessage(localizer.GetString("LastNameRequired"));
        RuleFor(x => x.Profile.Email).NotEmpty().WithMessage(localizer.GetString("EmailRequired"));
        RuleFor(x => x.Profile.BirthDate).NotEmpty().WithMessage(localizer.GetString("BirthDateRequired"));
        RuleFor(x => x.Profile.Active).NotNull().WithMessage("ActiveRequired");

        RuleFor(x => x.Profile.Password).NotEmpty().WithMessage(localizer.GetString("LoginPasswordRequired"))
            .MinimumLength(8).WithMessage(localizer.GetString("LoginPasswordLength"))
            .MaximumLength(64).WithMessage(localizer.GetString("LoginPasswordLength"))
            .When(x => !string.IsNullOrWhiteSpace(x.Profile.CurrentPassword));
        RuleFor(x => x.Profile.PasswordConfirm).NotEmpty().WithMessage(localizer.GetString("ConfirmLoginPasswordRequired"))
            .MinimumLength(8).WithMessage(localizer.GetString("LoginPasswordLength"))
            .MaximumLength(64).WithMessage(localizer.GetString("LoginPasswordLength"))
            .When(x => !string.IsNullOrWhiteSpace(x.Profile.CurrentPassword));
        RuleFor(x => x).Must(x => x.Profile.Password.Equals(x.Profile.PasswordConfirm)).WithMessage(localizer.GetString("PasswordPasswordConfirmDontMatch")).When(x => !string.IsNullOrWhiteSpace(x.Profile.CurrentPassword));
    }
}
