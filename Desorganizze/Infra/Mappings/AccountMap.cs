using Desorganizze.Models;
using FluentNHibernate.Mapping;

namespace Desorganizze.Infra.Mappings
{
    public class AccountMap : ClassMap<Account>
    {
        public AccountMap()
        {
            Id(acc => acc.Id).Column("id");
            References(acc => acc.User, "UserId");
            HasMany(x => x.Transactions);
            Table("accounts");
        }
    }
}
