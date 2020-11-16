using Desorganizze.Dtos;
using Desorganizze.Infra;
using Desorganizze.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace Desorganizze.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
            private readonly ISession _session;
            public UsersController(ISession session)
            {
                _session = session;
            }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var usuarios = await _session
                .Query<User>()
                .Select(x => new UserDto { Id = x.Id, Username = x.Username })
                .ToListAsync();

            return Ok(usuarios);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var userPersisted = await _session.Query<User>()
                .Where(x => x.Id == id)
                .Select(x => new UserDto { Username = x.Username })
                .FirstOrDefaultAsync();

            if (userPersisted == null)
                return NotFound();

            return Ok(userPersisted);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /users/login
        ///     {
        ///        "username": "vmamore",
        ///        "password": "mamore123"
        ///     }
        ///
        /// </remarks>
        /// <param name="userDto"></param>
        /// <returns>A token to access</returns>
        /// <response code="200">Returns a new token</response>
        /// <response code="404">The login/password aren't valid</response>  
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userPersisted = await _session
                .Query<User>()
                .FirstOrDefaultAsync(u => u.Username == userDto.Username &&
                                          u.Password == userDto.Password);

            if (userPersisted == null)
                return NotFound($"{userDto.Username} não existe.");

            var token = TokenService.GenerateToken(userPersisted);

            return Ok(new { username = userDto.Username, token });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userPersisted = await _session.Query<User>()
                .FirstOrDefaultAsync(u => u.Username == userDto.Username);

            if (userPersisted != null)
                return BadRequest($"{userDto.Username} já está sendo utilizado.");

            var user = new User(userDto.Username, userDto.Password);

            await _session.SaveAsync(user);

            userDto.Password = null;

            return Created($"account/{user.Id}", userDto);
        }
    }
}
