using ECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private IContext _context;
        private readonly ILogger<ProductController> _logger;

        public ReviewController(IContext context, ILogger<ProductController> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(int id)
        {
            _logger.LogInformation("api/review/{id} triggered");
            try
            {
                return Ok(await _context.Review.FindAsync(id));
                _logger.LogInformation("api/review/{id} completed successfully");
            }
            catch
            {
                return BadRequest();
                _logger.LogWarning("api/review/{id} completed with errors");
            }
        }

        [HttpPost()]
        public async Task<ActionResult<Review>> PostReview([FromBody]Review review)
        {
            _logger.LogInformation("posting a review");
            try
            {
                await _context.Review.AddAsync(review);
                await _context.SaveAsync();
                var t = _context.Review.Where(r=> r.UserId == review.UserId
                                            && r.ProductId == review.ProductId
                                            && r.Comment == review.Comment
                                            && r.Rating == review.Rating).FirstOrDefault<Review>();
                //User t = _context.User.Where(x => x.UserPassword == newUser.UserPassword && x.UserEmail == newUser.UserEmail).FirstOrDefault<User>();
                return Ok(t.ReviewId);
                _logger.LogInformation("posting review completed successfully");
            }
            catch
            {
                return BadRequest();
                _logger.LogWarning("posting review completed with errors");
            }
        }
    }
}
