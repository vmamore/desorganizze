using FluentMigrator;

namespace Desorganizze.Infra.Migrations
{
    [Migration(1)]
    public class AddUserTable : Migration
    {
        public override void Up()
        {
            Create.Table("users")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("username").AsString(128).NotNullable()
                .WithColumn("password").AsString(128).NotNullable();

            Insert.IntoTable("users")
                .Row(new { username = "vmamore", password = "mamore123" });
        }
        public override void Down()
        {
            Delete.Table("users");
        }
    }
}
