using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeesCatalog.Data.Migrations
{
    public partial class TestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "OrganizationId", "Name" },
                values: new object[] { 1, "Тестовая организация 1" });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "OrganizationId", "Name" },
                values: new object[] { 2, "Тестовая организация 2" });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "OrganizationId", "Name" },
                values: new object[] { 3, "Тестовая организация 3" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "Name", "OrganizationId" },
                values: new object[,]
                {
                    { 1, "Департамент 1", 1 },
                    { 2, "Департамент 2", 1 },
                    { 3, "Департамент 3", 1 },
                    { 4, "Департамент 1", 2 },
                    { 5, "Департамент 2", 2 },
                    { 6, "Департамент 1", 3 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DepartmentId", "Email", "FirstName", "Patronymic", "Surname" },
                values: new object[] { 1, 1, "ivanov@test.ru", "Иван", "Иванович", "Иванов" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DepartmentId", "Email", "FirstName", "Patronymic", "Surname" },
                values: new object[] { 2, 4, "petrov@test.ru", "Петр", "Петрович", "Петров" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DepartmentId", "Email", "FirstName", "Patronymic", "Surname" },
                values: new object[] { 3, 6, "sidorov@test.ru", "Сидор", "Сидорович", "Сидоров" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "OrganizationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "OrganizationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "OrganizationId",
                keyValue: 3);
        }
    }
}
