using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeBankingMindHub.Migrations
{
    public partial class addCardEntityFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FormDate",
                table: "Cards",
                newName: "FromDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FromDate",
                table: "Cards",
                newName: "FormDate");
        }
    }
}
