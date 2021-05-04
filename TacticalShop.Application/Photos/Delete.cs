using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TacticalShop.Application.Core;
using TacticalShop.Application.Interfaces;
using TacticalShop.Persistence;

namespace TacticalShop.Application.Photos
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string id { get; set; }
            public int productid { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DatabaseContext _context;
            private readonly IPhotoAccessor _photoAccessor;

            public Handler(DatabaseContext context, IPhotoAccessor photoAccessor)
            {
                _context = context;
                _photoAccessor = photoAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.Include(p => p.Photos)
                   .FirstOrDefaultAsync(x => x.ProductId.Equals(request.productid));
                if (product == null) return null;

                var photo = product.Photos.FirstOrDefault(x => x.Id == request.id);
                if (photo == null) return null;

                // if (photo.IsMain) return Result<Unit>.Failure("You cannot delete your product main photo");

                var result = await _photoAccessor.DeletePhoto(photo.Id);
                if (result == null) return Result<Unit>.Failure("Problem deleting photo from Cloudinary");
                product.Photos.Remove(photo);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("Failed deleting photo from API");
            }
        }
    }
}