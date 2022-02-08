using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyBook.DataAccess.Migrations
{
    public partial class renameProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {          
            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {           
            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");
        }
    }
}
