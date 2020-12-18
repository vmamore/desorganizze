using Desorganizze.Models;
using FluentNHibernate.Mapping;

namespace Desorganizze.Infra.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id).GeneratedBy.Identity().Column("id");
            Map(x => x.Username).Column("username");
            Map(x => x.Password).Column("password");
            Component(x => x.CPF, m =>
            {
                m.Map(x => x.Valor, "cpf");
            });
            Component(x => x.Name, m =>
            {
                m.Map(x => x.FirstName, "first_name");
                m.Map(x => x.LastName, "last_name");
            });
            HasOne(x => x.Account).PropertyRef(x => x.User).Cascade.All();
            Table("users");
        }
    }
}
