using System.Threading.Tasks;
using Desorganizze.Application.Commands.Login;
using Desorganizze.Dtos;
using Desorganizze.Infra.CQRS.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Desorganizze.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public LoginController(ILogger<LoginController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
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
            _logger.LogInformation("LoginDto: {@loginDto}", loginDto);

            if (!ModelState.IsValid) return BadRequest();

            var resultado = await _commandDispatcher.ExecuteAsync(new AuthenticateCommand(
                loginDto.Username, loginDto.Password));

            _logger.LogInformation("Response: {@response}", resultado);

            return Ok(resultado.ReturnDto);
        }
    }
}
