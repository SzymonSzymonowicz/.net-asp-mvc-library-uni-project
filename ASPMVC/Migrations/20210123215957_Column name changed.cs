using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPMVC.Migrations
{
    public partial class Columnnamechanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishingDate",
                table: "Books",
                newName: "PublicationDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicationDate",
                table: "Books",
                newName: "PublishingDate");
        }
    }
}
