using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TacticalShop.Application.Core;
using TacticalShop.Persistence;

namespace TacticalShop.Application.Categories
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DatabaseContext _context;

            public Handler(DatabaseContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories.FindAsync(request.id);

                if (category == null) return null;

                _context.Remove(category);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete category");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}