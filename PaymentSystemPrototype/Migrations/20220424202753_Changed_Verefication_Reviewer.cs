using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentSystemPrototype.Migrations
{
    /// <inheritdoc />
    public partial class Changed_Verefication_Reviewer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE verefications ALTER COLUMN reviewer TYPE integer USING (reviewer::integer);");

            migrationBuilder.CreateIndex(
                name: "IX_verefications_reviewer",
                table: "verefications",
                column: "reviewer",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_verefications_users_reviewer",
                table: "verefications",
                column: "reviewer",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_verefications_users_reviewer",
                table: "verefications");

            migrationBuilder.DropIndex(
                name: "IX_verefications_reviewer",
                table: "verefications");

            migrationBuilder.AlterColumn<string>(
                name: "reviewer",
                table: "verefications",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
