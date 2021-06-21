namespace Desorganizze.Infra.Migrations
{
    using FluentMigrator;

    [Migration(9)]
    public class CreateDefaultCategory : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE OR REPLACE FUNCTION create_categories() RETURNS setof bigint
                            language plpgsql as $$
                        DECLARE 
                            wallet_id integer;
                            wallets_cursor CURSOR FOR SELECT id FROM public.wallets; 
                        BEGIN
                            FOR walletRow IN wallets_cursor loop
	                        INSERT INTO public.categories (description, wallet_id) VALUES ('General', walletRow.id);
	                        RETURN NEXT wallet_id;
                            END loop;
                        END $$;
							
                        select create_categories();");

            Execute.Sql("DROP FUNCTION create_categories()");
        }
    }
}
