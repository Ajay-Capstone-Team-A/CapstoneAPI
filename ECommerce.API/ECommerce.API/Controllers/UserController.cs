using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(IContext context, ILogger<UserController> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        //gets a specific user, reference by id
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            _logger.LogInformation("api/user/{id} triggered");
            try
            {
                return Ok(await _context.User.FindAsync(id));
                _logger.LogInformation("api/user/{id} completed successfully");
            }
            catch
            {
                return BadRequest();
                _logger.LogWarning("api/user/{id} completed with errors");
            }
        }

        //returns a list of all users in the db
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            try
            {
                return Ok(await _context.User.ToListAsync());
                _logger.LogInformation("api/user/ generated a list of all users");
            }
            catch
            {
                return BadRequest();
                _logger.LogInformation("api/user/ had an error");
            }
        }

 

        //user_id field can be omitted in the request 
        [HttpPost()]
        public async Task<IActionResult> PostUser(User user)
        {
            //looks for this exact user in the db to avoid duplicates
            var duplicate = await _context.User.Where(u =>  u.UserEmail == user.UserEmail
                                               && u.UserFirstName == user.UserFirstName
                                               && u.UserLastName == user.UserLastName
                                               && u.UserPassword == user.UserPassword).ToListAsync();
            //if no duplicate
            if(duplicate.Count()==0)
            {
                _context.User.Add(user);
                await _context.SaveAsync();
                return CreatedAtAction("GetUser", new { id = user.UserId }, user);
            }
            else
            {
                return NoContent();
            }
        }
        
        /*
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateUser(ProfileDTO profileInfo,int id)
        {
            var findUser = await _context.User.Where(u => u.UserId == id).FirstOrDefaultAsync();

            //if this user doesnt exist in db
            if(findUser == null)
            {
                return BadRequest();
            } else
            {
                
                //_context.Entry(findUser).State = EntityState.Modified;
                return Ok();
            }
        } */
    }
}
