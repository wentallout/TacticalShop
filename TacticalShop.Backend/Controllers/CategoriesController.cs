using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TacticalShop.Application.Categories;
using TacticalShop.Persistence;
using TacticalShop.ViewModels;

namespace TacticalShop.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class CategoriesController : BaseApiController
    {
        private readonly DatabaseContext _context;

        public CategoriesController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CategoryVm>>> GetCategory()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CategoryVm>> GetCategory(int id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { id = id }));
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CategoryVm>> PutCategory(int id, CategoryCreateRequest categoryCreateRequest)
        {
            return HandleResult(await Mediator.Send(new Edit.Command { id = id, categoryCreateRequest = categoryCreateRequest }));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<CategoryVm>> PostCategory(CategoryCreateRequest categoryCreateRequest)
        {
            return HandleResult(await Mediator.Send(new Create.Command { categoryCreateRequest = categoryCreateRequest }));
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { id = id }));
        }
    }
}