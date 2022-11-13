using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Homework_EfCore.Migrations
{
    public partial class Revised_Entities_And_Add_method : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_Name",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Books_Name",
                table: "Books",
                column: "Name",
                unique: true);
        }
    }
}
