﻿using Desorganizze.Models.ValueObjects;

namespace Desorganizze.Models
{
    public class User
    {
        public virtual int Id { get; protected set; }
        public virtual string Username { get; protected set; }
        public virtual string Password { get; protected set; }
        public virtual Name Name { get; set; }
        public virtual CPF CPF { get; set; }
        public virtual Account Account { get; protected set; }

        protected User() {}

        public User(string firstName, string lastName, string cpf, string username, string password)
        {
            Name = Name.Create(firstName, lastName);
            CPF = CPF.Create(cpf);
            Username = username;
            Password = password;
            Account = new Account(this);
        }
    }
}
