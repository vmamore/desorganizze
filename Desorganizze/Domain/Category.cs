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

        public static Guid DefaultId() => Guid.Parse("8418db28-d240-11eb-b8bc-0242ac130003");

        public static Category Default => new Category
        {
            Id = Guid.Parse("8418db28-d240-11eb-b8bc-0242ac130003")
        };
        
    }
}
