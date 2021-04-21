using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TacticalShop.Backend.Data;
using TacticalShop.Backend.Models;
using TacticalShop.Backend.Services;
using TacticalShop.ViewModels;

namespace TacticalShop.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IStorageService _storageService;
        private readonly ILogger _logger;

        public ProductsController(DatabaseContext context, IStorageService storageService, ILogger<ProductsController> logger)
        {
            _context = context;
            _storageService = storageService;
            _logger = logger;
        }

        // GET: api/Products
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductVm>>> GetProducts()
        {
            var product = await _context.Products.Select(x => new
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

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductVm>> GetProduct(int id)
        {
            var product = await _context.Products.Include(x => x.Brand).Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.ProductId.Equals(id));

            if (product == null)
            {
                return NotFound();
            }

            var productVm = new ProductVm
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductDescription = product.ProductDescription,
                ProductImageName = product.ProductImageName,
                BrandName = product.Brand.BrandName,
                CategoryName = product.Category.CategoryName,
                ProductQuantity = product.ProductQuantity,
                CreatedDate = product.CreatedDate,
                UpdatedDate = product.UpdatedDate,
                StarRating = product.StarRating
            };

            productVm.ProductImageName = _storageService.GetFileUrl(product.ProductImageName);

            return productVm;
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PutProduct(int id, ProductCreateRequest productCreateRequest)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.ProductName = productCreateRequest.ProductName;

            product.ProductImageName = product.ProductImageName;
            product.ProductPrice = productCreateRequest.ProductPrice;
            product.ProductDescription = productCreateRequest.ProductDescription;
            product.ProductQuantity = productCreateRequest.ProductQuantity;
            product.BrandId = productCreateRequest.BrandId;
            product.CategoryId = productCreateRequest.CategoryId;
            product.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostProduct(  /*[FromForm]*/ ProductCreateRequest productCreateRequest)
        {
            var product = new Product
            {
                ProductName = productCreateRequest.ProductName,
                ProductDescription = productCreateRequest.ProductDescription,
                ProductQuantity = productCreateRequest.ProductQuantity,
                ProductPrice = productCreateRequest.ProductPrice,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                BrandId = productCreateRequest.BrandId,
                CategoryId = productCreateRequest.CategoryId,
            };

            if (productCreateRequest.ProductImage != null)
            {
                product.ProductImageName = await SaveFile(productCreateRequest.ProductImage);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { ProductId = product.ProductId }, null);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        [HttpGet("filterproducts")]
        [AllowAnonymous]
        public async Task<ActionResult<IList<ProductVm>>> GetFilteredProducts(int? categoryid = null, int? brandid = null)
        {
            var queryable = _context.Products.AsQueryable().AsNoTracking();

            if (categoryid != null)
            {
                queryable = _context.Products.Where(x => x.CategoryId == categoryid);
            }

            if (brandid != null)
            {
                queryable = _context.Products.Where(x => x.BrandId == brandid);
            }

            var product = await queryable.Select(x => new
            {
                x.ProductId,
                x.ProductName,
                x.ProductPrice,
                x.ProductDescription,
                x.ProductQuantity,
                x.CreatedDate,
                x.UpdatedDate,
                x.BrandId,
                x.CategoryId,
                x.ProductImageName,
                x.Category.CategoryName,
                x.Brand.BrandName
            }).ToListAsync();

            var productVm = product.Select(x => new ProductVm
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductPrice = x.ProductPrice,
                ProductDescription = x.ProductDescription,
                ProductImageName = x.ProductImageName,
                ProductQuantity = x.ProductQuantity,
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                BrandId = x.BrandId,
                BrandName = x.BrandName,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate
            }).ToList();

            return productVm;
        }
    }
}