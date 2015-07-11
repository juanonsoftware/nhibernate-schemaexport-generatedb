using FluentMigrator;

namespace NhibMigrations.Migrate
{
    [Migration(1)]
    public class UserTableMigration : Migration
    {
        public override void Down()
        {
            Delete.Table("User");
        }

        public override void Up()
        {
            Create.Table("User").WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Identity()
                .WithColumn("UserName").AsString().NotNullable().Unique();
        }
    }
}
