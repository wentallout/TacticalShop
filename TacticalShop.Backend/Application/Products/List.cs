using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TacticalShop.Backend.Data;
using TacticalShop.Backend.Services;
using TacticalShop.ViewModels;

namespace TacticalShop.Backend.Application.Products
{
    public class List
    {
        public class Query : IRequest<List<ProductVm>> { }

        public class Handler : IRequestHandler<Query, List<ProductVm>>
        {
            private readonly DatabaseContext _context;
            private readonly IStorageService _storageService;

            public Handler(DatabaseContext context, IStorageService storageService)
            {
                _context = context;
                _storageService = storageService;
            }

            public async Task<List<ProductVm>> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.Include(x => x.Brand).Include(x => x.Category).Select(x => new
                {
                    x.ProductId,
                    x.CategoryId,
                    x.BrandId,
                    x.ProductName,
                    x.ProductPrice,
                    x.ProductDescription,
                    x.ProductQuantity,
                    x.CreatedDate,
                    x.UpdatedDate,
                    x.Brand.BrandName,
                    x.Category.CategoryName,
                    x.ProductImageName,
                    x.StarRating
                }).AsNoTracking()
                    .ToListAsync();

                var productVm = product.Select(x => new ProductVm
                {
                    ProductId = x.ProductId,
                    CategoryId = x.CategoryId,
                    BrandId = x.BrandId,
                    ProductName = x.ProductName,
                    ProductPrice = x.ProductPrice,
                    ProductDescription = x.ProductDescription,
                    ProductQuantity = x.ProductQuantity,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    BrandName = x.BrandName,
                    CategoryName = x.CategoryName,
                    StarRating = x.StarRating,
                    ProductImageName = _storageService.GetFileUrl(x.ProductImageName)
                }).ToList();

                return productVm;
            }
        }
    }
}