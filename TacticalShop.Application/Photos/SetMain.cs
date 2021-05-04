using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TacticalShop.Application.Core;
using TacticalShop.Persistence;

namespace TacticalShop.Application.Photos
{
    public class SetMain
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string id { get; set; }
            public int productid { get; set; }
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
                var product = await _context.Products.Include(p => p.Photos)
                    .FirstOrDefaultAsync(x => x.ProductId == request.productid);
                if (product == null) return null;

                var photo = product.Photos.FirstOrDefault(x => x.Id == request.id);

                if (photo == null) return null;

                var currentMain = product.Photos.FirstOrDefault(x => x.IsMain);

                if (currentMain != null)
                    currentMain.IsMain = false;

                photo.IsMain = true;

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("Problem setting main photo");
            }
        }
    }
}