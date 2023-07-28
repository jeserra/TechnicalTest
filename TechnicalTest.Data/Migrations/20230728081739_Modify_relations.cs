using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnicalTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class Modify_relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Products");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Categories_Code",
                table: "Categories",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryCode",
                table: "Products",
                column: "CategoryCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryCode",
                table: "Products",
                column: "CategoryCode",
                principalTable: "Categories",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryCode",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryCode",
                table: "Products");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Categories_Code",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryID",
                table: "Products",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
