using FluentMigrator;

namespace Desorganizze.Infra.Migrations
{
    [Migration(6)]
    public class CreateWalletsForExistingUsers : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE OR REPLACE FUNCTION create_wallets() RETURNS setof bigint
                            language plpgsql as $$
                        DECLARE 
                            user_id integer;
                            users_cursor CURSOR FOR SELECT id FROM public.users; 
                        BEGIN
                            FOR userRow IN users_cursor loop
	                        INSERT INTO public.wallets (id, user_id) VALUES ((SELECT md5(random()::text || clock_timestamp()::text)::uuid), userRow.id);
	                        RETURN NEXT user_id;
                            END loop;
                        END $$;
							
                        select create_wallets();");
        }
    }
}
