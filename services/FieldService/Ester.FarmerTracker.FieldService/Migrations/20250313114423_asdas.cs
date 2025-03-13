using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ester.FarmerTracker.FieldService.Migrations
{
    /// <inheritdoc />
    public partial class asdas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FieldsSquereMeterSum",
                table: "Customers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldsSquereMeterSum",
                table: "Customers");
        }
    }
}
