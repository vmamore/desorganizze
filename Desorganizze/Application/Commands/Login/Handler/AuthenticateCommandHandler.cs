using Desorganizze.Domain.Repositories;
using Desorganizze.Infra;
using Desorganizze.Infra.CQRS.Commands;
using System.Threading.Tasks;

namespace Desorganizze.Application.Commands.Login.Handler
{
    public class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;

        public AuthenticateCommandHandler(IUserRepository userRepository,
            IWalletRepository walletRepository)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
        }

        public async Task<Result> ExecuteAsync(AuthenticateCommand command)
        {
            var userPersisted = await _userRepository.GetUserByUsernameAndPasswordAsync(command.Username, command.Password);

            if (userPersisted == null)
                return Result.Fail($"{command.Username} does not exist.");

            var token = TokenService.GenerateToken(userPersisted);

            var wallet = await _walletRepository.GetWalletByUserId(userPersisted.Id);

            var response = new
            {
                username = command.Username,
                name = userPersisted.Name.ToString(),
                cpf = userPersisted.CPF.ToString(),
                walletId = wallet.Id,
                token
            };

            return Result.Ok(response);
        }
    }
}
