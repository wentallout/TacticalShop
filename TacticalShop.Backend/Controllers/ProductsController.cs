using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TacticalShop.Backend.Data;
using TacticalShop.Backend.Models;
using TacticalShop.ViewModels;

namespace TacticalShop.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ProductsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductVm>>> GetProduct()
        {
            return await _context.Products
                .Select(x => new ProductVm {ProductId = x.ProductId,BrandId=x.BrandId,CategoryId = x.CategoryId,ProductName=x.ProductName,ProductDescription=x.ProductDescription,ProductImage=x.ProductImage,ProductPrice=x.ProductPrice,ProductQuantity=x.ProductQuantity})
                .ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductVm>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var productVm = new ProductVm
            {
                ProductId = product.ProductId,
                ProductName=product.ProductName,
                ProductPrice=product.ProductPrice,
                ProductDescription=product.ProductDescription,
                ProductImage=product.ProductImage,
                BrandId=product.BrandId,
                CategoryId = product.CategoryId,
                ProductQuantity=product.ProductQuantity

            };

            return productVm;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> PutProduct(int id, ProductCreateRequest productCreateRequest)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.ProductName = productCreateRequest.ProductName;
            product.ProductImage = productCreateRequest.ProductImage;
            product.ProductPrice = productCreateRequest.ProductPrice;
            product.ProductDescription = productCreateRequest.ProductDescription;
            product.ProductQuantity = productCreateRequest.ProductQuantity;
            product.BrandId = productCreateRequest.BrandId;
            product.CategoryId = productCreateRequest.CategoryId;
         
            await _context.SaveChangesAsync();
            return NoContent();
        }

       
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ProductVm>> PostProduct(ProductCreateRequest productCreateRequest)
        {
            var product = new Product
            {
                ProductName = productCreateRequest.ProductName,
                BrandId = productCreateRequest.BrandId,
                CategoryId = productCreateRequest.CategoryId,
                ProductImage = productCreateRequest.ProductImage,
                ProductDescription = productCreateRequest.ProductDescription,
                ProductQuantity = productCreateRequest.ProductQuantity,
                ProductPrice = productCreateRequest.ProductPrice,
                CreatedDate = productCreateRequest.CreatedDate,
                UpdatedDate = productCreateRequest.UpdatedDate,



            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct",
                new { ProductId = product.ProductId },
                new ProductVm { ProductId  = product.ProductId,
                    ProductName  = product.ProductName,
                    ProductPrice=product.ProductPrice,
                    ProductDescription=product.ProductDescription,
                    ProductImage=product.ProductImage,
                    BrandId=product.BrandId,
                    CategoryId=product.CategoryId,
                    ProductQuantity=product.ProductQuantity,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = product.UpdatedDate

                    

                });
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
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
    }
}
