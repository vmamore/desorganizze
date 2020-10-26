namespace Desorganizze.Models
{
    public class User
    {
        public virtual int Id { get; private set; }
        public virtual string Username { get; private set; }
        public virtual string Password { get; private set; }
        public virtual Account Account { get; private set; }

        protected User() {}

        public User(string username, string password)
        {
            Username = username;
            Password = password;
            Account = new Account(this);
        }
    }
}
