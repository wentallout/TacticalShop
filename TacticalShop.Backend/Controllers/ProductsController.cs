using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TacticalShop.Application.Core;
using TacticalShop.Application.Products;
using TacticalShop.Persistence;
using TacticalShop.ViewModels;

namespace TacticalShop.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ProductsController : BaseApiController
    {
        private readonly DatabaseContext _context;

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
        [AllowAnonymous]
        public async Task<IActionResult> PostProduct(ProductCreateRequest productCreateRequest)
        {
            return HandleResult(await Mediator.Send(new Create.Command { productCreateRequest = productCreateRequest }));
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PutProduct(int id, ProductCreateRequest productCreateRequest)
        {
            return HandleResult(await Mediator.Send(new Edit.Command { id = id, productCreateRequest = productCreateRequest }));
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { id = id }));
        }
    }
}