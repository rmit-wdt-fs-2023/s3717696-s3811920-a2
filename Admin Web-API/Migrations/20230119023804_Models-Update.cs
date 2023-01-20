using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCBAAdmin.Migrations
{
    /// <inheritdoc />
    public partial class ModelsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payee_Address_AddressID",
                table: "Payee");

            migrationBuilder.DropIndex(
                name: "IX_Payee_AddressID",
                table: "Payee");

            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "Payee");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Payee",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Payee",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "Payee",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Payee",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Payee");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Payee");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "Payee");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Payee");

            migrationBuilder.AddColumn<int>(
                name: "AddressID",
                table: "Payee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payee_AddressID",
                table: "Payee",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payee_Address_AddressID",
                table: "Payee",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
