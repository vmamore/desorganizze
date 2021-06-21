using FluentMigrator;

namespace Desorganizze.Infra.Migrations
{
    [Migration(8)]
    public class CreateCategories : Migration
    {
        public override void Up()
        {
            Create.Table("categories")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("description").AsString(32).NotNullable()
                .WithColumn("wallet_id").AsGuid().NotNullable();

            Create.ForeignKey("FK_categories_wallets_id")
                .FromTable("categories").ForeignColumn("wallet_id")
                .ToTable("wallets").PrimaryColumn("id");

            Alter.Table("transactions")
                .AddColumn("category_id").AsInt64().NotNullable();

            Create.ForeignKey("FK_transactions_categories_id")
                .FromTable("transactions").ForeignColumn("category_id")
                .ToTable("categories").PrimaryColumn("id");
        }

        public override void Down() => Delete.Table("categories");
    }
}
