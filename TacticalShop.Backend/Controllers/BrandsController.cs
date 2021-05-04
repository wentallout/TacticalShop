using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TacticalShop.Domain;
using TacticalShop.Persistence;
using TacticalShop.ViewModels;

namespace TacticalShop.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class BrandsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public BrandsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Brands
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BrandVm>>> GetBrand()
        {
            return await _context.Brands
                .Select(x => new BrandVm { BrandId = x.BrandId, BrandName = x.BrandName }).AsNoTracking()
                .ToListAsync();
        }

        // GET: api/Brands/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<BrandVm>> GetBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            var brandVm = new BrandVm
            {
                BrandId = brand.BrandId,
                BrandName = brand.BrandName
            };

            return brandVm;
        }

        // PUT: api/Brands/5

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<BrandVm>> PutBrand(int id, BrandCreateRequest brandCreateRequest)
        {
            var brand = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            brand.CategoryName = brandCreateRequest.BrandName;

            if (id != brand.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<BrandVm>> PostBrand(BrandCreateRequest brandCreateRequest)
        {
            var brand = new Brand
            {
                BrandName = brandCreateRequest.BrandName
            };

            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrand", new { BrandId = brand.BrandId }, new BrandVm { BrandId = brand.BrandId, BrandName = brand.BrandName });
        }

        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brandVm = await _context.Brands.FindAsync(id);
            if (brandVm == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brandVm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.BrandId == id);
        }
    }
}