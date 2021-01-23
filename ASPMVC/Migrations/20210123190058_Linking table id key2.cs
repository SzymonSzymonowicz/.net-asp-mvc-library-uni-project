using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPMVC.Migrations
{
    public partial class Linkingtableidkey2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BookAuthors",
                newName: "BookAuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookAuthorId",
                table: "BookAuthors",
                newName: "Id");
        }
    }
}
