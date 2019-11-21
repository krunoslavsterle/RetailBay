using FluentValidation;
using System;

namespace RetailBay.Application.Products.Commands.EditProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage($"Must define the {nameof(UpdateProductCommand.Name)} field")
                .MaximumLength(500).WithMessage($"Maximum length of {nameof(UpdateProductCommand.Name)} field is 500 charachters");

            RuleFor(v => v.Slug)
                .NotEmpty().WithMessage($"Must define the {nameof(UpdateProductCommand.Slug)} field")
                .MaximumLength(500).WithMessage($"Maximum length of {nameof(UpdateProductCommand.Slug)} field is 500 charachters");

            RuleFor(v => v.ProductCategoryId)
                .NotEqual(Guid.Empty);
        }
    }
}
