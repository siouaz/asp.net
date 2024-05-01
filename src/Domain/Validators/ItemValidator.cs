using FluentValidation;
using Microsoft.Extensions.Localization;

using OeuilDeSauron.Domain.Models;

namespace OeuilDeSauron.Domain.Validators;

public class ItemValidator : AbstractValidator<ItemModel>
{
    public ItemValidator(IStringLocalizer<Resources> localizer)
    {
        RuleFor(i => i.Name).NotEmpty().WithMessage(localizer.GetString("ItemNameRequired"));
        RuleFor(i => i.Code).NotEmpty().WithMessage(localizer.GetString("ItemCodeRequired"));
        RuleFor(i => i.ListId).NotEmpty().WithMessage(localizer.GetString("ItemListIdRequired"));
        RuleFor(i => i.Id).NotNull().WithMessage(localizer.GetString("ItemKeyRequired"));
    }
}
