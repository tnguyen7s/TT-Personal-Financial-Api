using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Personal_Financial_WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Identifier = table.Column<string>(type: "varchar(10)", nullable: false),
                    FullName = table.Column<string>(type: "varchar(50)", nullable: false),
                    TotalBalance = table.Column<decimal>(type: "money", nullable: false),
                    TotalSavingForGood = table.Column<decimal>(type: "money", nullable: false),
                    TotalDonated = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    UserIdentifier = table.Column<string>(type: "varchar(10)", nullable: false),
                    SentTo = table.Column<string>(type: "varchar(100)", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Amount = table.Column<short>(type: "smallint", nullable: false),
                    Comment = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => new { x.UserIdentifier, x.SentTo, x.Date });
                    table.ForeignKey(
                        name: "FK_Donations_Users_UserIdentifier",
                        column: x => x.UserIdentifier,
                        principalTable: "Users",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    UserIdentifier = table.Column<string>(type: "varchar(10)", nullable: false),
                    Category = table.Column<string>(type: "varchar(10)", nullable: false),
                    Month = table.Column<byte>(type: "tinyint", nullable: false),
                    Year = table.Column<short>(type: "smallint", nullable: false),
                    Spend = table.Column<short>(type: "smallint", nullable: false),
                    Limit = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => new { x.UserIdentifier, x.Category, x.Month, x.Year });
                    table.ForeignKey(
                        name: "FK_Expenses_Users_UserIdentifier",
                        column: x => x.UserIdentifier,
                        principalTable: "Users",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    UserIdentifier = table.Column<string>(type: "varchar(10)", nullable: false),
                    SecondStakeHolder = table.Column<string>(type: "varchar(50)", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Amount = table.Column<short>(type: "smallint", nullable: false),
                    Owned = table.Column<byte>(type: "tinyint", nullable: false),
                    Done = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => new { x.UserIdentifier, x.SecondStakeHolder, x.Date });
                    table.ForeignKey(
                        name: "FK_Loans_Users_UserIdentifier",
                        column: x => x.UserIdentifier,
                        principalTable: "Users",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Save4Goods",
                columns: table => new
                {
                    UserIdentifier = table.Column<string>(type: "varchar(10)", nullable: false),
                    Item = table.Column<string>(type: "varchar(100)", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Amount = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Save4Goods", x => new { x.UserIdentifier, x.Item, x.Date });
                    table.ForeignKey(
                        name: "FK_Save4Goods_Users_UserIdentifier",
                        column: x => x.UserIdentifier,
                        principalTable: "Users",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishItems",
                columns: table => new
                {
                    UserIdentifier = table.Column<string>(type: "varchar(10)", nullable: false),
                    Item = table.Column<string>(type: "varchar(100)", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Amount = table.Column<decimal>(type: "smallmoney", nullable: false),
                    Comment = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishItems", x => new { x.UserIdentifier, x.Item, x.Date });
                    table.ForeignKey(
                        name: "FK_WishItems_Users_UserIdentifier",
                        column: x => x.UserIdentifier,
                        principalTable: "Users",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_UserIdentifier_Date_SentTo",
                table: "Donations",
                columns: new[] { "UserIdentifier", "Date", "SentTo" });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserIdentifier_Category_Month_Year",
                table: "Expenses",
                columns: new[] { "UserIdentifier", "Category", "Month", "Year" });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_UserIdentifier_SecondStakeHolder_Date",
                table: "Loans",
                columns: new[] { "UserIdentifier", "SecondStakeHolder", "Date" });

            migrationBuilder.CreateIndex(
                name: "IX_Save4Goods_UserIdentifier_Item_Date",
                table: "Save4Goods",
                columns: new[] { "UserIdentifier", "Item", "Date" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Identifier",
                table: "Users",
                column: "Identifier");

            migrationBuilder.CreateIndex(
                name: "IX_WishItems_UserIdentifier_Item_Date",
                table: "WishItems",
                columns: new[] { "UserIdentifier", "Item", "Date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Save4Goods");

            migrationBuilder.DropTable(
                name: "WishItems");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
