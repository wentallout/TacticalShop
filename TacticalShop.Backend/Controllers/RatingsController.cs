using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
    public class RatingsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public RatingsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRatings()
        {
            return await _context.Ratings.ToListAsync();
        }

        // GET: api/Ratings/5
        [HttpGet("{userid}/{productid}")]
        public async Task<ActionResult<Rating>> GetRating(string userid, int productid)
        {
            var rating = await _context.Ratings.FirstOrDefaultAsync(x => x.UserId == userid && x.ProductId == productid);
            if (rating == null)
            {
                return rating;
            }
            var ratingvm = new RatingVm
            {
                UserId = rating.UserId,
                ProductId = rating.ProductId,
                Star = rating.Star,
            };
            return rating;
        }

        [HttpPut]
        public async Task<IActionResult> PutRating(RatingUpdateRequest ratingUpdateRequest)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var rating = await _context.Ratings.FirstOrDefaultAsync(x => x.ProductId == ratingUpdateRequest.ProductId && x.UserId == ratingUpdateRequest.UserId);
                if (rating == null)
                {
                    return NotFound();
                }
                rating.Star = ratingUpdateRequest.Star;
                rating.UpdatedDate = DateTime.Now;
                await _context.SaveChangesAsync();

                var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == ratingUpdateRequest.ProductId);
                var listRating = await _context.Ratings.Where(x => x.ProductId == ratingUpdateRequest.ProductId).ToListAsync();
                product.StarRating = Convert.ToInt32(listRating.Sum(x => x.Star) / listRating.Count);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
            }

            return Accepted();
        }


        [HttpPost]
        public async Task<ActionResult> PostRating(RatingCreateRequest ratingCreateRequest)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            var rating = new Rating
            {
                UserId = ratingCreateRequest.UserId,
                ProductId = ratingCreateRequest.ProductId,
                Star = ratingCreateRequest.Star,
                CreatedDate = DateTime.Now,

            };
            try
            {
                await _context.Ratings.AddAsync(rating);
                await _context.SaveChangesAsync();
                var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == ratingCreateRequest.ProductId);
                var listrating = await _context.Ratings.Where(x => x.ProductId == ratingCreateRequest.ProductId).ToListAsync();
                product.StarRating = listrating.Sum(x => x.Star) / listrating.Count;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            { }

            return Accepted();
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(string id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatingExists(string id)
        {
            return _context.Ratings.Any(e => e.UserId == id);
        }
    }
}
