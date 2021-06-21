using FluentMigrator;
using FluentMigrator.SqlServer;
using System;

namespace Desorganizze.Infra.Migrations
{
    [Migration(2)]
    public class AddAccountsTable : Migration
    {
        public override void Up()
        {
            Create.Table("accounts")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("user_id").AsInt64().ForeignKey("FK_accounts_users_id", "users", "id").NotNullable();

            Insert.IntoTable("accounts")
                .Row(new { id = Guid.NewGuid(), user_id = 1 });
        }

        public override void Down() => Delete.Table("accounts");
    }
}
