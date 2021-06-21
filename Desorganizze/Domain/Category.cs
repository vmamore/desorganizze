namespace Desorganizze.Domain
{
    using System;

    public class Category
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Description { get; }
        public virtual Wallet Wallet { get; }
        
        protected Category() { }

        public Category(string description, Wallet wallet)
        {
            Id = Guid.NewGuid();
            Description = description;
            Wallet = wallet;
        }

        public static Category Default(Wallet wallet) => new Category("General", wallet);
    }
}
