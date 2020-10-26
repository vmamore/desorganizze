using FluentMigrator;

namespace Desorganizze.Infra.Migrations
{
    [Migration(2020102222421126)]
    public class AddTransactionsTable : Migration
    {
        public override void Up()
        {
            Create.Table("transactions")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("type").AsByte().NotNullable()
                .WithColumn("created_date").AsDateTime().NotNullable()
                .WithColumn("money_amount").AsInt64().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("transactions");
        }
    }
}
