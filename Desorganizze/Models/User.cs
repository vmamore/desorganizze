namespace Desorganizze.Models
{
    public class User
    {
        public virtual int Id { get; protected set; }
        public virtual string Username { get; protected set; }
        public virtual string Password { get; protected set; }
        public virtual Account Account { get; protected set; }

        protected User() {}

        public User(string username, string password)
        {
            Username = username;
            Password = password;
            Account = new Account(this);
        }
    }
}
