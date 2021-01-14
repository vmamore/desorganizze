using Desorganizze.Domain;
using FluentNHibernate.Mapping;

namespace Desorganizze.Infra.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id).GeneratedBy.Identity().Column("id");

            Component(x => x.Password, m =>
            {
                m.Map(x => x.Valor, "password");
            });
            Component(x => x.Username, m =>
            {
                m.Map(x => x.Valor, "username");
            });
            Component(x => x.CPF, m =>
            {
                m.Map(x => x.Valor, "cpf");
            });
            Component(x => x.Name, m =>
            {
                m.Map(x => x.FirstName, "first_name");
                m.Map(x => x.LastName, "last_name");
            });
            HasOne(x => x.Wallet).PropertyRef(x => x.User).Cascade.All();
            Table("users");
        }
    }
}
