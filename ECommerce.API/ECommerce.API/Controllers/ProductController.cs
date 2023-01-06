using ECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPatch("/restock")]
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
    }
}
