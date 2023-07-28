using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeBankingMindHub.Migrations
{
    public partial class addTransactionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "Transactions",
                newName: "Description");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Transactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Transactions",
                newName: "description");
        }
    }
}
