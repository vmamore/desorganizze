using Desorganizze.Models;
using FluentNHibernate.Mapping;
    
namespace Desorganizze.Infra.Mappings
{
    public class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Assigned();
            Map(x => x.Type).CustomType<int>().Column("type");
            Map(x => x.CreatedDate).Column("created_date");
            References(x => x.Account)
                .Class<Account>()
                .Column("account_id");
            Component(x => x.TotalAmount, m =>
            {
                m.Map(x => x.Amount, "money_amount");
            });
            Table("transactions");
        }
    }
}
