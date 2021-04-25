using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TacticalShop.Backend.Data;
using TacticalShop.ViewModels;

namespace TacticalShop.Backend.Application.Products
{
    public class Edit
    {
        public class Command : IRequest
        {
            public ProductCreateRequest productCreateRequest { get; set; }

            public int id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DatabaseContext _context;

            public Handler(DatabaseContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FindAsync(request.id);

                product.ProductName = request.productCreateRequest.ProductName;

                product.ProductImageName = product.ProductImageName;
                product.ProductPrice = request.productCreateRequest.ProductPrice;
                product.ProductDescription = request.productCreateRequest.ProductDescription;
                product.ProductQuantity = request.productCreateRequest.ProductQuantity;
                product.BrandId = request.productCreateRequest.BrandId;
                product.CategoryId = request.productCreateRequest.CategoryId;
                product.UpdatedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}