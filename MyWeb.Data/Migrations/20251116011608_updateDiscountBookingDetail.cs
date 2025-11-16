using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateDiscountBookingDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "BookingDetails",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "BookingDetails");
        }
    }
}
