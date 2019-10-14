using Microsoft.EntityFrameworkCore.Migrations;

namespace DownTime.Data.Migrations
{
    public partial class IsSiteJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestIntervel",
                table: "WebSites",
                newName: "RequestInterval");

            migrationBuilder.AddColumn<bool>(
                name: "IsSetJob",
                table: "WebSites",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSetJob",
                table: "WebSites");

            migrationBuilder.RenameColumn(
                name: "RequestInterval",
                table: "WebSites",
                newName: "RequestIntervel");
        }
    }
}
