using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTT.Web.Data.Migrations
{
    public partial class AddPlanItemTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "PlanItems",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "PlanItems");
        }
    }
}
