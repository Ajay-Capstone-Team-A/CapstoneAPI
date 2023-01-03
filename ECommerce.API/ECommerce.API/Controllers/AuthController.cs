using ECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IContext _context;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Route("auth/register")]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] User newUser)
        {
            _logger.LogInformation("auth/register triggered");
            try
            {
                await _context.User.AddAsync(newUser);
                await _context.SaveAsync();
                var t = _context.User.Where(x => x.UserPassword == newUser.UserPassword && x.UserEmail == newUser.UserEmail).FirstOrDefault<User>();
                //User t = _context.User.Where(x => x.UserPassword == newUser.UserPassword && x.UserEmail == newUser.UserEmail).FirstOrDefault<User>();
                return Ok(t.UserId);
                _logger.LogInformation("auth/register completed successfully");
            }
            catch
            {
                return BadRequest();
                _logger.LogWarning("auth/register completed with errors");
            }
        }
        [Route("auth/login")]
        [HttpPost]
        public async Task<ActionResult<User>> Login([FromBody] UserDTO LR)
        {
            _logger.LogInformation("auth/login triggered");
            User t;
            try
            {
                 t =  _context.User.Where(x=>x.UserPassword == LR.password && x.UserEmail == LR.email).FirstOrDefault<User>();
                _logger.LogInformation("auth/login completed successfully");
            }
            catch
            {
                return BadRequest();
                _logger.LogWarning("auth/login completed with errors");
            }
            return t;
        }

        [Route("auth/logout")]
        [HttpPost]
        public ActionResult Logout()
        {
            _logger.LogInformation("auth/logout triggered");
            return Ok();
            _logger.LogInformation("auth/logout completed successfully");
        }

        [Route("auth/profile/{id}")]
        [HttpPatch]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            _context.User.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!_context.User.Any(e => e.UserId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
    }

}

    