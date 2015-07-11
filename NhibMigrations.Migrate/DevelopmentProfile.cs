using FluentMigrator;

namespace NhibMigrations.Migrate
{
    [Profile("Development")]
    public class DevelopmentProfile : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Insert.IntoTable("User").Row(new
            {
                UserName = "devuser1",
            });
        }
    }
}
