using ECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private  IContext _context;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IContext context, ILogger<ProductController> logger)
        {
            this._context = context;
            this._logger = logger;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetOne(int id)
        {
            _logger.LogInformation("api/product/{id} triggered");
            try
            {
                return Ok(await _context.Product.FindAsync(id));
                _logger.LogInformation("api/product/{id} completed successfully");
            }
            catch
            {
                return BadRequest();
                _logger.LogWarning("api/product/{id} completed with errors");
            }
        }

        [HttpGet]
        public async Task<ActionResult<Product[]>> GetAll()
        {
            _logger.LogInformation("api/product triggered");
            try
            {
                return Ok(_context.Product.ToList());
                _logger.LogInformation("api/product completed successfully");
            }
            catch
            {
                return BadRequest();
                _logger.LogWarning("api/product completed with errors");
            }
        }

        [HttpPatch]
        public async Task<ActionResult<Product[]>> Purchase([FromBody] ProductDTO[] purchaseProducts)
        {
            _logger.LogInformation("PATCH api/product triggered");
            List<Product> products = new List<Product>();
            try
            {
                foreach (ProductDTO item in purchaseProducts)
                {
                    Product tmp = await _context.Product.FindAsync(item.id);
                    if ((tmp.ProductQuantity - item.quantity) >= 0)
                    {
                        tmp.ProductQuantity = tmp.ProductQuantity - item.quantity;
                        _context.Product.Update(tmp);
                        _context.SaveAsync();
                        products.Add(await _context.Product.FindAsync(item.id));
                    }
                    else
                    {
                        throw new Exception("Insuffecient inventory.");
                    }
                }
                return Ok(products);
                _logger.LogInformation("PATCH api/product completed successfully");
            }
            catch
            {
                return BadRequest();
                _logger.LogWarning("PATCH api/product completed with errors");

            }


        }

        [HttpPatch("restock")]
        public async Task<ActionResult<Product[]>> Restock([FromBody] ProductDTO[] restockProducts)
        {
            _logger.LogInformation("PATCH api/product/restock triggered");
            List<Product> products = new List<Product>();
            try
            {
                foreach(ProductDTO item in restockProducts)
                {
                    Product tmp = await _context.Product.FindAsync(item.id);
                    tmp.ProductQuantity = tmp.ProductQuantity + item.quantity;
                    _context.Product.Update(tmp);
                    _context.SaveAsync();
                    products.Add(await _context.Product.FindAsync(item.id));
                }
                return Ok(products);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getReviews/{id}")]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> getReviews(int id)
        {
            List<ReviewDTO> reviewsDTO = new List<ReviewDTO>();
            var reviews = await _context.Review.Where(r => r.ProductId == id).ToListAsync();
            foreach(Review review in reviews)
            {
                var user = await _context.User.Where(u => u.UserId == review.UserId).FirstOrDefaultAsync();
                ReviewDTO reviewDTO = new ReviewDTO(review.ReviewId,user.UserFirstName, user.UserLastName, review.ProductId, review.Comment, review.Rating);
                reviewsDTO.Add(reviewDTO);
            }

            List<ReviewDTO> sorted = reviewsDTO.OrderBy(r => r.ReviewId).ToList();
            return sorted;
        }

        [HttpGet("getReviewAverage/{id}")]
        public async Task<ActionResult<double>> getReviewAverage(int id)
        {
            double average = 0;
            var reviews = await _context.Review.Where(r => r.ProductId == id).ToListAsync();
            List<int> scores = new List<int>();

            foreach(Review review in reviews)
                scores.Add(review.Rating);
            
            average = scores.Average();
            return average;
        }
    }
}
