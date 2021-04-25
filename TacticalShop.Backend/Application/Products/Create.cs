using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using TacticalShop.Backend.Data;
using TacticalShop.Backend.Models;
using TacticalShop.Backend.Services;
using TacticalShop.ViewModels;

namespace TacticalShop.Backend.Application.Products
{
    public class Create
    {
        public class Command : IRequest
        {
            public ProductCreateRequest productCreateRequest { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DatabaseContext _context;
            private readonly IStorageService _storageService;

            public Handler(DatabaseContext context, IStorageService storageService)
            {
                _context = context;
                _storageService = storageService;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = new Product()
                {
                    ProductName = request.productCreateRequest.ProductName,
                    ProductDescription = request.productCreateRequest.ProductDescription,
                    ProductQuantity = request.productCreateRequest.ProductQuantity,
                    ProductPrice = request.productCreateRequest.ProductPrice,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    BrandId = request.productCreateRequest.BrandId,
                    CategoryId = request.productCreateRequest.CategoryId,
                };

                if (request.productCreateRequest.ProductImage != null)
                {
                    product.ProductImageName = await SaveFile(request.productCreateRequest.ProductImage);
                }

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }

            private async Task<string> SaveFile(IFormFile file)
            {
                var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
                await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
                return fileName;
            }
        }
    }
}