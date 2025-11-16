using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Products_ProductId",
                table: "BookingDetails");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "BookingDetails",
                newName: "AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingDetails_ProductId",
                table: "BookingDetails",
                newName: "IX_BookingDetails_AssetId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Bookings",
                type: "int",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Assets_AssetId",
                table: "BookingDetails",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "AssetId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Assets_AssetId",
                table: "BookingDetails");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "BookingDetails",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingDetails_AssetId",
                table: "BookingDetails",
                newName: "IX_BookingDetails_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Products_ProductId",
                table: "BookingDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
