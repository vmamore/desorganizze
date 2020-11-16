﻿using Desorganizze.Models;
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
            HasOne(x => x.Account).PropertyRef(x => x.User).Cascade.All();
            Table("users");
        }
    }
}