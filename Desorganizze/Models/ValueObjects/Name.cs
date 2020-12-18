using System;

namespace Desorganizze.Models
{
    public class Name
    {
        public virtual string FirstName { get; private set; }
        public virtual string LastName { get; private set; }

        private Name(){}
        private Name(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentNullException(nameof(firstName));

            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentNullException(nameof(lastName));

            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public static Name Create(string firstName, string lastName)
        {
            return new Name(firstName, lastName);
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}