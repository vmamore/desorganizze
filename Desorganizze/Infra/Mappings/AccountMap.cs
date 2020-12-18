using Desorganizze.Models;
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
            References(x => x.User, "user_id").Unique();
            HasMany(x => x.Transactions)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .KeyColumn("account_id")
                .Inverse()
                .Cascade.SaveUpdate();
            Table("accounts");
        }
    }
}
