using FluentMigrator;

namespace Desorganizze.Infra.Migrations
{
    [Migration(5)]
    public class AddingTableWallet : Migration
    {
        public override void Up()
        {
            Create.Table("wallets")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("user_id").AsInt64().ForeignKey("FK_wallets_users_id", "users", "id").NotNullable();
        }

        public override void Down()
        {
            Delete.Table("wallets");
        }
    }
}
