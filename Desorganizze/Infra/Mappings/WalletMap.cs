using Desorganizze.Domain;
using FluentNHibernate.Mapping;

namespace Desorganizze.Infra.Mappings
{
    public class WalletMap : ClassMap<Wallet>
    {
        public WalletMap()
        {
            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Assigned();
            References(x => x.User, "user_id").Unique();
            HasMany(x => x.Accounts)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .KeyColumn("wallet_id")
                .Inverse()
                .Cascade.SaveUpdate();
            Table("wallets");
        }
    }
}
