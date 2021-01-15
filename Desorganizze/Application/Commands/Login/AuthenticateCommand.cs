using Desorganizze.Infra.CQRS.Commands;

namespace Desorganizze.Application.Commands.Login
{
    public class AuthenticateCommand : ICommand
    {
        public AuthenticateCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }
        public string Password { get; }
    }
}
