using FluentMigrator;
using System;

namespace Desorganizze.Infra.Migrations
{
    [Migration(2)]
    public class AddAccountsTable : Migration
    {
        public override void Up()
        {
            Create.Table("accounts")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("user_id").AsInt64().ForeignKey("FK_accounts_users_id", "users", "id").NotNullable();
        }

        public override void Down()
        {
            Delete.Table("accounts");
        }
        
    }
}
