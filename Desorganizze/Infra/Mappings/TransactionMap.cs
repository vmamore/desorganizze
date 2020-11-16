using Desorganizze.Models;
using FluentNHibernate.Mapping;
    
namespace Desorganizze.Infra.Mappings
{
    public class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Id(x => x.Id).Column("id");
            Map(x => x.Type).Column("type");
            Map(x => x.CreatedDate).Column("created_date");
            References(x => x.Account)
                .Column("account_id")
                .Cascade.All();
            Component(x => x.TotalAmount, m =>
            {
                m.Map(x => x.Amount, "money_amount");
            });
            Table("transactions");
        }
    }
}
