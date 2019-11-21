using FluentValidation;

namespace RetailBay.Application.ProductCategories.Commands.UpdateProductCategory
{
    public class UpdateProductCategoryCommandValidator : AbstractValidator<UpdateProductCategoryCommand>
    {
        public UpdateProductCategoryCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage($"Must define the {nameof(UpdateProductCategoryCommand.Name)} field")
                .MaximumLength(100).WithMessage($"Maximum length of {nameof(UpdateProductCategoryCommand.Name)} field is 100 charachters");

            RuleFor(v => v.Abrv)
                    .NotEmpty().WithMessage($"Must define the {nameof(UpdateProductCategoryCommand.Abrv)} field")
                    .MaximumLength(20).WithMessage($"Maximum length of {nameof(UpdateProductCategoryCommand.Abrv)} field is 20 charachters");

            RuleFor(v => v.Slug)
                    .NotEmpty().WithMessage($"Must define the {nameof(UpdateProductCategoryCommand.Slug)} field")
                    .MaximumLength(100).WithMessage($"Maximum length of {nameof(UpdateProductCategoryCommand.Slug)} field is 100 charachters");
        }
    }
}
