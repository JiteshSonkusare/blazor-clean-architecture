using Application.Features.Products.Commands.Upsert;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Validators.Features.Products.Commands.Upsert
{
    public class ProductUpsertCommandValidator : AbstractValidator<ProductUpsertCommand>
    {
        public ProductUpsertCommandValidator(IStringLocalizer<ProductUpsertCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
            RuleFor(request => request.Barcode)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Barcode is required!"]);
            RuleFor(request => request.Description)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Description is required!"]);
            RuleFor(request => request.BrandId)
                .NotEmpty().WithMessage(x => localizer["Brand is required!"]);
            RuleFor(request => request.Rate)
                .GreaterThan(0).WithMessage(x => localizer["Rate must be greater than 0"]);
        }
    }
}
