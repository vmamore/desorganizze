using FluentMigrator;

namespace Desorganizze.Infra.Migrations
{
    [Migration(7)]
    public class ChangingAccountsUsersRelationshipToWallets : Migration
    {
        public override void Up()
        {
            Alter.Table("accounts")
                .AddColumn("wallet_id").AsGuid().ForeignKey("FK_accounts_wallets_id", "wallets", "id").Nullable();

            Execute.Sql(@"UPDATE public.accounts as A
	                        SET wallet_id = w.id
                        FROM public.wallets AS W
                        where A.user_id = W.user_id");

            Delete.Column("user_id").FromTable("accounts");

            Execute.Sql("DROP FUNCTION create_wallets()");
        }

        public override void Down()
        {
            Delete.Column("wallet_id").FromTable("accounts");

            Alter.Table("accounts")
                .AddColumn("user_id").AsInt64().ForeignKey("FK_accounts_users_id", "users", "id").NotNullable();
        }
    }
}
