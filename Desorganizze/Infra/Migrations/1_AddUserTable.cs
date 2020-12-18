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
                .WithColumn("password").AsString(128).NotNullable()
                .WithColumn("cpf").AsString(14).NotNullable()
                .WithColumn("first_name").AsString(256).NotNullable()
                .WithColumn("last_name").AsString(256).NotNullable();

            Insert.IntoTable("users")
                .Row(new { username = "vmamore", 
                           password = "teste123",
                           cpf = "96240191000",
                           first_name = "Vinícius",
                           last_name = "Mamoré" });
        }
        public override void Down()
        {
            Delete.Table("users");
        }
    }
}
