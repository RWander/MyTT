using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTT.Web.Data.Migrations
{
    public partial class AddPlanItemUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PlanItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PlanItems");
        }
    }
}
