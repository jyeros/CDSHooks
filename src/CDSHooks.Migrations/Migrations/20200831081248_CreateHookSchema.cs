using Microsoft.EntityFrameworkCore.Migrations;

namespace CDSHooks.Migrations.Migrations
{
    public partial class CreateHookSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hook",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Workflow = table.Column<string>(nullable: true),
                    Context = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hook", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hook");
        }
    }
}
