using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TacticalShop.Application.Photos;

namespace TacticalShop.Backend.Controllers
{
    public class PhotosController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> PostPhoto([FromForm] Add.Command command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [HttpDelete("deletephoto/{id}/{productid}")]
        public async Task<IActionResult> DeletePhoto(string id, int productid)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { id = id, productid = productid }));
        }

        [HttpPost("setMain/{id}/{productid}")]
        public async Task<IActionResult> setMain(string id, int productid)
        {
            return HandleResult(await Mediator.Send(new SetMain.Command { id = id, productid = productid }));
        }
    }
}