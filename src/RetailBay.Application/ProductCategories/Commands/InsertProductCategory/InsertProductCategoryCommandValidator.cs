using FluentValidation;

namespace RetailBay.Application.ProductCategories.Commands.InsertProductCategory
{
    public class InsertProductCategoryCommandValidator : AbstractValidator<InsertProductCategoryCommand>
    {
        public InsertProductCategoryCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage($"Must define the {nameof(InsertProductCategoryCommand.Name)} field")
                .MaximumLength(100).WithMessage($"Maximum length of {nameof(InsertProductCategoryCommand.Name)} field is 100 charachters");

            RuleFor(v => v.Abrv)
                .NotEmpty().WithMessage($"Must define the {nameof(InsertProductCategoryCommand.Abrv)} field")
                .MaximumLength(20).WithMessage($"Maximum length of {nameof(InsertProductCategoryCommand.Abrv)} field is 20 charachters");

            RuleFor(v => v.Slug)
                .NotEmpty().WithMessage($"Must define the {nameof(InsertProductCategoryCommand.Slug)} field")
                .MaximumLength(100).WithMessage($"Maximum length of {nameof(InsertProductCategoryCommand.Slug)} field is 100 charachters");
        }
    }
}
