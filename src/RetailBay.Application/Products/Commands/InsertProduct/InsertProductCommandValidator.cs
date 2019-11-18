using FluentValidation;
using System;

namespace RetailBay.Application.Products.Commands.InsertProduct
{
    public class InsertProductCommandValidator : AbstractValidator<InsertProductCommand>
    {
        public InsertProductCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage($"Must define the {nameof(InsertProductCommand.Name)} field")
                .MaximumLength(500).WithMessage($"Maximum length of {nameof(InsertProductCommand.Name)} field is 500 charachters");

            RuleFor(v => v.Slug)
                .NotEmpty().WithMessage($"Must define the {nameof(InsertProductCommand.Slug)} field")
                .MaximumLength(500).WithMessage($"Maximum length of {nameof(InsertProductCommand.Slug)} field is 500 charachters");

            RuleFor(v => v.ProductCategoryId)
                .NotEqual(Guid.Empty);
        }
    }
}
