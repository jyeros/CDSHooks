using Microsoft.EntityFrameworkCore.Migrations;

namespace CDSHooks.Migrations.Migrations
{
    public partial class RenameHookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Hook",
                newName: "Hooks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Hooks",
                newName: "Hook");
        }
    }
}
