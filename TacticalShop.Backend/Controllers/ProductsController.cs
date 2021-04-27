using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TacticalShop.Backend.Application.Core;
using TacticalShop.Backend.Application.Products;
using TacticalShop.Backend.Data;
using TacticalShop.Backend.Services;
using TacticalShop.ViewModels;

namespace TacticalShop.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ProductsController : BaseApiController
    {
        private readonly DatabaseContext _context;
        private readonly IStorageService _storageService;
        private readonly ILogger _logger;

        public ProductsController(DatabaseContext context, ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Products
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductVm>>> GetProducts([FromQuery] PagingParams param, int? categoryid = null, int? brandid = null)
        {
            return HandleResult(await Mediator.Send(new List.Query { Params = param, categoryid = categoryid, brandid = brandid }));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProduct(int id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct(  /*[FromForm]*/ ProductCreateRequest productCreateRequest)
        {
            return HandleResult(await Mediator.Send(new Create.Command { productCreateRequest = productCreateRequest }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductCreateRequest productCreateRequest)
        {
            return HandleResult(await Mediator.Send(new Edit.Command { id = id, productCreateRequest = productCreateRequest }));
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { id = id }));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
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