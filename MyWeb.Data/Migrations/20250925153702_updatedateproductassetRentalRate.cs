using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedateproductassetRentalRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashBooks_Products_ProductId",
                table: "CashBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Products_ProductId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ProductId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_CashBooks_ProductId",
                table: "CashBooks");

            migrationBuilder.DropColumn(
                name: "PricePerDay",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PricePerHour",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CashBooks");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "Specification");

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    AssetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.AssetId);
                    table.ForeignKey(
                        name: "FK_Assets_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentalRates",
                columns: table => new
                {
                    RateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    AssetId = table.Column<int>(type: "int", nullable: true),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalRates", x => x.RateId);
                    table.ForeignKey(
                        name: "FK_RentalRates_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "AssetId");
                    table.ForeignKey(
                        name: "FK_RentalRates_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: (byte)0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ProductId",
                table: "Assets",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRates_AssetId",
                table: "RentalRates",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRates_ProductId",
                table: "RentalRates",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalRates");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.RenameColumn(
                name: "Specification",
                table: "Products",
                newName: "Description");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerDay",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerHour",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "CashBooks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_ProductImage_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: (byte)1);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ProductId",
                table: "Payments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CashBooks_ProductId",
                table: "CashBooks",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashBooks_Products_ProductId",
                table: "CashBooks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Products_ProductId",
                table: "Payments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }
    }
}
