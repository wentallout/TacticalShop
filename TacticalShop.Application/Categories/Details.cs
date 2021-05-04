using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TacticalShop.Application.Core;
using TacticalShop.Persistence;
using TacticalShop.ViewModels;

namespace TacticalShop.Application.Categories
{
    public class Details
    {
        public class Query : IRequest<Result<CategoryVm>>
        {
            public int id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<CategoryVm>>
        {
            private readonly DatabaseContext _context;

            public Handler(DatabaseContext context)
            {
                _context = context;
            }

            public async Task<Result<CategoryVm>> Handle(Query request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.CategoryId.Equals(request.id));

                var categoryVm = new CategoryVm
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    CategoryDescription = category.CategoryDescription,
                };

                return Result<CategoryVm>.Success(categoryVm);
            }
        }
    }
}