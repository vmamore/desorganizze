using Desorganizze.Application.Commands.Users;
using Desorganizze.Application.Queries.Users.Parameters;
using Desorganizze.Application.Queries.Users.ReadModel;
using Desorganizze.Dtos;
using Desorganizze.Infra.CQRS.Commands;
using Desorganizze.Infra.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desorganizze.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandDispatcher _commandDispatcher;
        public UsersController(IQueryProcessor queryProcessor, ICommandDispatcher commandDispatcher)
        {
            _queryProcessor = queryProcessor;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var getAllUsers = new GetAllUsers();

            var result = await _queryProcessor.ExecuteQueryAsync<GetAllUsers, IEnumerable<UserDtoItem>>(getAllUsers);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var getUserById = new GetUserById(id);

            var result = await _queryProcessor.ExecuteQueryAsync<GetUserById, UserDtoItem>(getUserById);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _commandDispatcher.ExecuteAsync(
                new RegisterUser(
                    userDto.FirstName,
                    userDto.LastName,
                    userDto.CPF,
                    userDto.Username,
                    userDto.Password));

            if (result.Failure) return BadRequest(result.ErrorMessage);

            userDto.Password = null;

            return Created($"account/{0}", userDto);
        }
    }
}
