using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TacticalShop.Application.Core;
using TacticalShop.Persistence;
using TacticalShop.ViewModels;

namespace TacticalShop.Application.Products
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public ProductCreateRequest productCreateRequest { get; set; }

            public int id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DatabaseContext _context;

            public Handler(DatabaseContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FindAsync(request.id);
                if (product == null) return null;

                product.ProductName = request.productCreateRequest.ProductName;

                product.ProductImageName = product.ProductImageName;
                product.ProductPrice = request.productCreateRequest.ProductPrice;
                product.ProductDescription = request.productCreateRequest.ProductDescription;
                product.ProductQuantity = request.productCreateRequest.ProductQuantity;
                product.BrandId = request.productCreateRequest.BrandId;
                product.CategoryId = request.productCreateRequest.CategoryId;
                product.UpdatedDate = DateTime.UtcNow;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to edit/update product");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}