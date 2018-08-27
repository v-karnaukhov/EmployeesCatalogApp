using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeesCatalog.Data.Migrations
{
    public partial class AddEmployeesDepartmentsChangesHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepartmentsChangesHistories",
                columns: table => new
                {
                    EntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    CurrentDepartmentId = table.Column<int>(nullable: false),
                    NewDepartmentId = table.Column<int>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentsChangesHistories", x => x.EntryId);
                    table.ForeignKey(
                        name: "FK_DepartmentsChangesHistories_Departments_CurrentDepartmentId",
                        column: x => x.CurrentDepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentsChangesHistories_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentsChangesHistories_Departments_NewDepartmentId",
                        column: x => x.NewDepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsChangesHistories_CurrentDepartmentId",
                table: "DepartmentsChangesHistories",
                column: "CurrentDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsChangesHistories_EmployeeId",
                table: "DepartmentsChangesHistories",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsChangesHistories_NewDepartmentId",
                table: "DepartmentsChangesHistories",
                column: "NewDepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentsChangesHistories");
        }
    }
}
