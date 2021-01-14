using Desorganizze.Infra.CQRS.Commands;

namespace Desorganizze.Application.Commands.Users
{
    public class RegisterUser : ICommand
    {
        public RegisterUser(string firstName, string lastName, string cPF, string username, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            CPF = cPF;
            Username = username;
            Password = password;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string CPF { get; }
        public string Username { get; }
        public string Password { get; }
    }
}
