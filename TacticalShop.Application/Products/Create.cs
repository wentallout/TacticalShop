using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using TacticalShop.Application.Core;
using TacticalShop.Application.Services;
using TacticalShop.Domain;
using TacticalShop.Persistence;
using TacticalShop.ViewModels;

namespace TacticalShop.Application.Products
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public ProductCreateRequest productCreateRequest { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.productCreateRequest).SetValidator(new ProductValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DatabaseContext _context;
            private readonly IStorageService _storageService;

            public Handler(DatabaseContext context, IStorageService storageService)
            {
                _context = context;
                _storageService = storageService;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = new Product
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
                else
                {
                    product.ProductImageName = "";
                }

                _context.Products.Add(product);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create product");

                return Result<Unit>.Success(Unit.Value);
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