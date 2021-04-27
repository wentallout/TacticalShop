using FluentValidation;
using TacticalShop.ViewModels;

namespace TacticalShop.Backend.Application.Products
{
    public class ProductValidator : AbstractValidator<ProductCreateRequest>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty();
        }
    }
}