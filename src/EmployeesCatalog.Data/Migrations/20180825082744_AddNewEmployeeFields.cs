using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeesCatalog.Data.Migrations
{
    public partial class AddNewEmployeeFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActual",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Sex",
                table: "Employees",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsActual",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Employees");
        }
    }
}
