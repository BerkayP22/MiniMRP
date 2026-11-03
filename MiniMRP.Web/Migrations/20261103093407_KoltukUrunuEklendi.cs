using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMRP.Web.Migrations
{
    /// <inheritdoc />
    public partial class KoltukUrunuEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 2L, "KOLT", "Koltuk" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
