using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TacticalShop.Application.Photos;

namespace TacticalShop.Application.Interfaces
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> AddPhoto(IFormFile file);

        Task<string> DeletePhoto(string publicId);
    }
}