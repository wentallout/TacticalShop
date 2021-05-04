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
    public class List
    {
        public class Query : IRequest<Result<PagedList<ProductVm>>>
        {
            public PagingParams Params { get; set; }

            public int? brandid { get; set; }
            public int? categoryid { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<ProductVm>>>
        {
            private readonly DatabaseContext _context;
            private readonly IStorageService _storageService;

            public Handler(DatabaseContext context, IStorageService storageService)
            {
                _context = context;
                _storageService = storageService;
            }

            public async Task<Result<PagedList<ProductVm>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = _context.Products.Include(x => x.Photos).Include(x => x.Brand).Include(x => x.Category).AsQueryable().AsNoTracking();

                if (request.categoryid != null)
                {
                    queryable = _context.Products.Where(x => x.CategoryId == request.categoryid);
                }

                if (request.brandid != null)
                {
                    queryable = _context.Products.Where(x => x.BrandId == request.brandid);
                }

                var productVm = queryable.Select(x => new ProductVm
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
                    BrandName = x.Brand.BrandName,
                    CategoryName = x.Category.CategoryName,
                    StarRating = x.StarRating,
                    PhotoUrl = x.Photos.Select(x => x.Url).FirstOrDefault(),

                    ProductImageName = _storageService.GetFileUrl(x.ProductImageName)
                }).AsQueryable();

                return Result<PagedList<ProductVm>>.Success(await PagedList<ProductVm>.CreateAsync(productVm, request.Params.PageNumber, request.Params.PageSize));
            }
        }
    }
}