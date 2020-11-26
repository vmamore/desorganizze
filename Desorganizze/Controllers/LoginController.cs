using System.Threading.Tasks;
using Desorganizze.Dtos;
using Desorganizze.Infra;
using Desorganizze.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Linq;

namespace Desorganizze.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ISession _session;
        public LoginController(ISession session)
        {
            _session = session;
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
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userPersisted = await _session
                .Query<User>()
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username &&
                                          u.Password == loginDto.Password);

            if (userPersisted == null)
                return NotFound($"{loginDto.Username} não existe.");

            var token = TokenService.GenerateToken(userPersisted);

            return Ok(new { username = loginDto.Username, token });
        }
    }
}
