using FluentMigrator;

namespace Desorganizze.Infra.Migrations
{
    [Migration(3)]
    public class AddTransactionsTable : Migration
    {
        public override void Up()
        {
            Create.Table("transactions")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("type").AsByte().NotNullable()
                .WithColumn("created_date").AsDateTime().NotNullable()
                .WithColumn("money_amount").AsInt64().NotNullable()
                .WithColumn("account_id").AsGuid().NotNullable();

            Create.ForeignKey("FK_transactions_accounts_id")
                .FromTable("transactions").ForeignColumn("account_id")
                .ToTable("accounts").PrimaryColumn("id");
        }

        public override void Down()
        {
            Delete.Table("transactions");
        }
    }
}
