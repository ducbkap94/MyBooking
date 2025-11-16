using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "BookingDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RateType",
                table: "BookingDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "BookingDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "RateType",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "BookingDetails");
        }
    }
}
