using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCBAWeb.Migrations
{
    /// <inheritdoc />
    public partial class Modelsupdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transaction_DestinationAccountNumber",
                table: "Transaction",
                column: "DestinationAccountNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Account_DestinationAccountNumber",
                table: "Transaction",
                column: "DestinationAccountNumber",
                principalTable: "Account",
                principalColumn: "AccountNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Account_DestinationAccountNumber",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_DestinationAccountNumber",
                table: "Transaction");
        }
    }
}
