using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Personal_Financial_WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddExpenseIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WishItems_UserIdentifier",
                table: "WishItems",
                column: "UserIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Save4Goods_UserIdentifier",
                table: "Save4Goods",
                column: "UserIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_UserIdentifier",
                table: "Loans",
                column: "UserIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserIdentifier",
                table: "Expenses",
                column: "UserIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserIdentifier_Month_Year",
                table: "Expenses",
                columns: new[] { "UserIdentifier", "Month", "Year" });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_UserIdentifier",
                table: "Donations",
                column: "UserIdentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WishItems_UserIdentifier",
                table: "WishItems");

            migrationBuilder.DropIndex(
                name: "IX_Save4Goods_UserIdentifier",
                table: "Save4Goods");

            migrationBuilder.DropIndex(
                name: "IX_Loans_UserIdentifier",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_UserIdentifier",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_UserIdentifier_Month_Year",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Donations_UserIdentifier",
                table: "Donations");
        }
    }
}
