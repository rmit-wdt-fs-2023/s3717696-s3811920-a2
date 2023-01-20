using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCBAAdmin.Migrations
{
    /// <inheritdoc />
    public partial class ModelsUpdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Login_CustomerID",
                table: "Login");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Payee",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Payee",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Login_CustomerID",
                table: "Login",
                column: "CustomerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Login_CustomerID",
                table: "Login");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Payee",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Phone",
                table: "Payee",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Login_CustomerID",
                table: "Login",
                column: "CustomerID",
                unique: true);
        }
    }
}
