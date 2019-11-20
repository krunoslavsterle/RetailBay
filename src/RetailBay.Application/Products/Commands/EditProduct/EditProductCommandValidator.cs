using FluentValidation;
using System;

namespace RetailBay.Application.Products.Commands.EditProduct
{
    public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
    {
        public EditProductCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage($"Must define the {nameof(EditProductCommand.Name)} field")
                .MaximumLength(500).WithMessage($"Maximum length of {nameof(EditProductCommand.Name)} field is 500 charachters");

            RuleFor(v => v.Slug)
                .NotEmpty().WithMessage($"Must define the {nameof(EditProductCommand.Slug)} field")
                .MaximumLength(500).WithMessage($"Maximum length of {nameof(EditProductCommand.Slug)} field is 500 charachters");

            RuleFor(v => v.ProductCategoryId)
                .NotEqual(Guid.Empty);
        }
    }
}
