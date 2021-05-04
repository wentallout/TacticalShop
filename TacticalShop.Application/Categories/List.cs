using System.Collections.Generic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TacticalShop.Persistence;
using TacticalShop.ViewModels;

namespace TacticalShop.Application.Categories
{
    public class List
    {
        public class Query : IRequest<List<CategoryVm>>
        {
        }

        public class Handler : IRequestHandler<Query, List<CategoryVm>>
        {
            private readonly DatabaseContext _context;

            public Handler(DatabaseContext context)
            {
                _context = context;
            }

            public async Task<List<CategoryVm>> Handle(Query request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories.Select(x => new
                {
                    x.CategoryId,
                    x.CategoryName,
                    x.CategoryDescription
                }).AsNoTracking().ToListAsync();

                var categoryVm = category.Select(x => new CategoryVm()
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.CategoryName,
                    CategoryDescription = x.CategoryDescription
                }).ToList();

                return categoryVm;
            }
        }
    }
}