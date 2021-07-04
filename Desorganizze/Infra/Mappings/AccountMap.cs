using Desorganizze.Domain;
using FluentNHibernate.Mapping;

namespace Desorganizze.Infra.Mappings
{
    public class AccountMap : ClassMap<Account>
    {
        public AccountMap()
        {
            Id(acc => acc.Id)
                .Column("id")
                .GeneratedBy.Assigned();
            References(x => x.Wallet, "wallet_id").Unique();
            Component(x => x.Name, m =>
            {
                m.Map(x => x.Value, "name");
            });
            HasMany(x => x.Transactions)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .KeyColumn("account_id")
                .Inverse()
                .Cascade.SaveUpdate();
            Table("accounts");
        }
    }
}
