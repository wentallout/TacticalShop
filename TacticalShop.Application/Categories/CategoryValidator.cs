using FluentValidation;
using TacticalShop.ViewModels;

namespace TacticalShop.Application.Categories
{
    public class CategoryValidator : AbstractValidator<CategoryCreateRequest>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty();
        }
    }
}