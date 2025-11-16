using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Bookings");

            migrationBuilder.AddColumn<decimal>(
                name: "DebtAmount",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Days",
                table: "BookingDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BookingDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DebtAmount",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Days",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BookingDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
