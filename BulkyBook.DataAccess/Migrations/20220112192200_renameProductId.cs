using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyBook.DataAccess.Migrations
{
    public partial class renameProductId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProduntId",
                table: "OrderDetails",
                newName: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {        
            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderDetails",
                newName: "ProduntId");
        }
    }
}
