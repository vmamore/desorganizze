using FluentMigrator;

namespace Desorganizze.Infra.Migrations
{
    [Migration(4)]
    public class AddingNameInAccounts : Migration
    {
        public override void Up() => Alter.Table("accounts")
                .AddColumn("name").AsString(50).WithDefaultValue("Principal").NotNullable();

        public override void Down() => Delete.Column("name").FromTable("accounts");
    }
}
