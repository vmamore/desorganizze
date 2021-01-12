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
        /// <param name="loginDto"></param>
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
                .FirstOrDefaultAsync(u => u.Username.Valor == loginDto.Username &&
                                          u.Password.Valor == loginDto.Password);

            if (userPersisted == null)
                return NotFound($"{loginDto.Username} não existe.");

            var token = TokenService.GenerateToken(userPersisted);

            var wallet = await _session
                .Query<Wallet>()
                .FirstOrDefaultAsync(w => w.User.Id == userPersisted.Id);

            return Ok(new { 
                username = loginDto.Username, 
                name = userPersisted.Name.ToString(), 
                cpf = userPersisted.CPF.ToString(), 
                walletId = wallet.Id, 
                token });
        }
    }
}
