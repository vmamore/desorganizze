using Desorganizze.Domain;
using Desorganizze.Domain.Repositories;
using Desorganizze.Infra.CQRS.Commands;
using System.Threading.Tasks;

namespace Desorganizze.Application.Commands.Users.Handlers
{
    public class RegisterUserHandler : ICommandHandler<RegisterUser>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> ExecuteAsync(RegisterUser command)
        {
            var userPersisted = await _userRepository.GetUserByNameAsync(command.Username);

            if (userPersisted != null) return Result.Fail($"{command.Username} is already been used.");

            var user = new User(
                command.FirstName,
                command.LastName,
                command.CPF,
                command.Username,
                command.Password);

            await _userRepository.SaveAsync(user);

            return Result.Ok();
        }
    }
}
