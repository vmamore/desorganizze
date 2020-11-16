using Desorganizze.Models;
using FluentNHibernate.Mapping;

namespace Desorganizze.Infra.Mappings
{
    public class AccountMap : ClassMap<Account>
    {
        public AccountMap()
        {
            Id(acc => acc.Id)
                .GeneratedBy.Identity()
                .Column("id");
            References(x => x.User, "user_id").Unique();
            HasMany(x => x.Transactions)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore);
            Table("accounts");
        }
    }
}
