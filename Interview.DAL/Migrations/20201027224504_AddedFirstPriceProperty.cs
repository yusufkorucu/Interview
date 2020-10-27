using Microsoft.EntityFrameworkCore.Migrations;

namespace Interview.DAL.Migrations
{
    public partial class AddedFirstPriceProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstPrice",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstPrice",
                table: "Products");
        }
    }
}
