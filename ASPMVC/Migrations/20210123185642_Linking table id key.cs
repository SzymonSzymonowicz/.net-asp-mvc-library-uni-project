using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPMVC.Migrations
{
    public partial class Linkingtableidkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BookAuthors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "BookAuthors");
        }
    }
}
