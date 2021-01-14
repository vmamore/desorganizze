using Desorganizze.Domain.ValueObjects;

namespace Desorganizze.Domain
{
    public class User
    {
        public virtual int Id { get; protected set; }
        public virtual Username Username { get; protected set; }
        public virtual Password Password { get; protected set; }
        public virtual Name Name { get; set; }
        public virtual CPF CPF { get; set; }
        public virtual Wallet Wallet { get; protected set; }

        protected User() {}

        public User(string firstName, string lastName, string cpf, string username, string password)
        {
            Name = Name.Create(firstName, lastName);
            CPF = CPF.Create(cpf);
            Username = Username.Create(username);
            Password = Password.Create(password);
            Wallet = new Wallet(this);
        }
    }
}
