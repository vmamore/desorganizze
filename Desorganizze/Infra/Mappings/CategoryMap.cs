namespace Desorganizze.Infra.Mappings
{
    using Desorganizze.Domain;
    using FluentNHibernate.Mapping;

    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned().Column("id");
            Map(x => x.Description).Column("description");
            References(x => x.Wallet)
                .Class<Wallet>()
                .Column("wallet_id");
            Table("categories");
        }
    }
}
