using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TacticalShop.Application.Core;
using TacticalShop.Application.Interfaces;
using TacticalShop.Domain;
using TacticalShop.Persistence;

namespace TacticalShop.Application.Photos
{
    public class Add
    {
        public class Command : IRequest<Result<Photo>>
        {
            public IFormFile File { get; set; }
            public int productid { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Photo>>
        {
            private readonly DatabaseContext _context;
            private readonly IPhotoAccessor _photoAccessor;

            public Handler(DatabaseContext context, IPhotoAccessor photoAccessor)
            {
                _context = context;
                _photoAccessor = photoAccessor;
            }

            public async Task<Result<Photo>> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.Include(p => p.Photos)
                    .FirstOrDefaultAsync(x => x.ProductId.Equals(request.productid));

                if (product == null) return null;

                var photoUploadResult = await _photoAccessor.AddPhoto(request.File);

                var photo = new Photo
                {
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId
                };

                if (!product.Photos.Any(x => x.IsMain)) photo.IsMain = true;

                product.Photos.Add(photo);

                var result = await _context.SaveChangesAsync() > 0;

                if (result) return Result<Photo>.Success(photo);

                return Result<Photo>.Failure("Problem adding photo");
            }
        }
    }
}