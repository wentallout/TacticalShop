using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TacticalShop.Backend.Data;
using TacticalShop.Backend.Models;
using TacticalShop.ViewModels;

namespace TacticalShop.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                .Select(x => new CategoryVm { CategoryId = x.CategoryId, CategoryName = x.CategoryName }).AsNoTracking()
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
                CategoryName = category.CategoryName
            };

            return categoryVm;
        }



        [HttpPut("{id}")]

        public async Task<IActionResult> PutCategory(int id, CategoryVm categoryVm)
        {
            if (id != categoryVm.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(categoryVm).State = EntityState.Modified;

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

        public async Task<ActionResult<CategoryVm>> PostCategory(CategoryCreateRequest categoryCreateRequest)
        {
            var category = new Category
            {
                CategoryName = categoryCreateRequest.CategoryName
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { CategoryId = category.CategoryId }, new CategoryVm { CategoryId = category.CategoryId, CategoryName = category.CategoryName });
        }


        [HttpDelete("{id}")]

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
