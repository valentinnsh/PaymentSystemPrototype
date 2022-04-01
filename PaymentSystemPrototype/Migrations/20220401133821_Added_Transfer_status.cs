using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentSystemPrototype.Migrations
{
    /// <inheritdoc />
    public partial class Added_Transfer_status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "fund_transfers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "fund_transfers");
        }
    }
}
