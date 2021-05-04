using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TacticalShop.Application.Core;
using TacticalShop.Application.Services;
using TacticalShop.Persistence;
using TacticalShop.ViewModels;

namespace TacticalShop.Application.Products
{
    public class Details
    {
        public class Query : IRequest<Result<ProductVm>>
        {
            public int id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ProductVm>>
        {
            private readonly DatabaseContext _context;
            private readonly IStorageService _storageService;

            public Handler(DatabaseContext context, IStorageService storageService)
            {
                _context = context;
                _storageService = storageService;
            }

            public async Task<Result<ProductVm>> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.Include(x => x.Photos).Include(x => x.Brand).Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.ProductId.Equals(request.id));

                var productVm = new ProductVm
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductDescription = product.ProductDescription,
                    ProductImageName = product.ProductImageName,
                    BrandName = product.Brand.BrandName,
                    CategoryName = product.Category.CategoryName,
                    BrandId = product.BrandId,
                    CategoryId = product.CategoryId,
                    ProductQuantity = product.ProductQuantity,
                    CreatedDate = product.CreatedDate,
                    UpdatedDate = product.UpdatedDate,
                    StarRating = product.StarRating,
                    PhotoUrl = product.Photos.Select(x => x.Url).FirstOrDefault(),
                };

                productVm.ProductImageName = _storageService.GetFileUrl(product.ProductImageName);

                return Result<ProductVm>.Success(productVm);
            }
        }
    }
}