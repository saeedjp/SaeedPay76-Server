using Microsoft.EntityFrameworkCore.Migrations;

namespace SaeedPay76.Data.Migrations
{
    public partial class AddOwnerNameToBank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "BankCards",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "BankCards");
        }
    }
}
