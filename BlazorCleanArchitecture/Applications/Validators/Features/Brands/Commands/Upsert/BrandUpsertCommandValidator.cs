using Application.Features.Brands.Commands.Upsert;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Validators.Features.Brands.Commands.Upsert
{
    public class BrandUpsertCommandValidator : AbstractValidator<BrandUpsertCommand>
    {
        public BrandUpsertCommandValidator(IStringLocalizer<BrandUpsertCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
            RuleFor(request => request.Description)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Description is required!"]);
        }
    }
}
