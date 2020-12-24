using Desorganizze.Dtos;
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
                .Select(x => new UserDto { Id = x.Id, Username = x.Username.Valor })
                .ToListAsync();

            return Ok(usuarios);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var userPersisted = await _session.Query<User>()
                .Where(x => x.Id == id)
                .Select(x => new UserDto { Username = x.Username.Valor })
                .FirstOrDefaultAsync();

            if (userPersisted == null)
                return NotFound();

            return Ok(userPersisted);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userPersisted = await _session.Query<User>()
                .FirstOrDefaultAsync(u => u.Username.Valor == userDto.Username);

            if (userPersisted != null)
                return BadRequest($"{userDto.Username} já está sendo utilizado.");

            var user = new User(
                userDto.FirstName,
                userDto.LastName,
                userDto.CPF,
                userDto.Username, 
                userDto.Password);

            await _session.SaveAsync(user);

            userDto.Password = null;

            return Created($"account/{user.Id}", userDto);
        }
    }
}
