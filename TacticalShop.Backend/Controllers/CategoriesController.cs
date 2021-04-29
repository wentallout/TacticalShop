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
    public class CategoriesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CategoriesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CategoryVm>>> GetCategory()
        {
            return await _context.Categories
                .Select(x => new CategoryVm { CategoryId = x.CategoryId, CategoryName = x.CategoryName, CategoryDescription = x.CategoryDescription }).AsNoTracking()
                .ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CategoryVm>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryVm = new CategoryVm
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription
            };

            return categoryVm;
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CategoryVm>> PutCategory(int id, CategoryCreateRequest categoryCreateRequest)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            category.CategoryName = categoryCreateRequest.CategoryName;
            category.CategoryDescription = categoryCreateRequest.CategoryDescription;

            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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
        [AllowAnonymous]
        public async Task<ActionResult<CategoryVm>> PostCategory(CategoryCreateRequest categoryCreateRequest)
        {
            var category = new Category
            {
                CategoryName = categoryCreateRequest.CategoryName,
                CategoryDescription = categoryCreateRequest.CategoryDescription,
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { category.CategoryId }, new CategoryVm { CategoryId = category.CategoryId, CategoryName = category.CategoryName });
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryVm = await _context.Categories.FindAsync(id);
            if (categoryVm == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categoryVm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}