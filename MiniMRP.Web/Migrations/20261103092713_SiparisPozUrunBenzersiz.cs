using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMRP.Web.Migrations
{
    /// <inheritdoc />
    public partial class SiparisPozUrunBenzersiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_No_PositionNo",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_No_PositionNo_ProductId",
                table: "Orders",
                columns: new[] { "No", "PositionNo", "ProductId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_No_PositionNo_ProductId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_No_PositionNo",
                table: "Orders",
                columns: new[] { "No", "PositionNo" },
                unique: true);
        }
    }
}
