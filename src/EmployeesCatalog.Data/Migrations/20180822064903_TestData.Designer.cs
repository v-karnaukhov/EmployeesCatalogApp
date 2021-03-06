﻿// <auto-generated />
using EmployeesCatalog.Data.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeesCatalog.Data.Migrations
{
    [DbContext(typeof(EmployeesContext))]
    [Migration("20180822064903_TestData")]
    partial class TestData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmployeesCatalog.Data.Entities.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("OrganizationId");

                    b.HasKey("DepartmentId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Departments");

                    b.HasData(
                        new { DepartmentId = 1, Name = "Департамент 1", OrganizationId = 1 },
                        new { DepartmentId = 2, Name = "Департамент 2", OrganizationId = 1 },
                        new { DepartmentId = 3, Name = "Департамент 3", OrganizationId = 1 },
                        new { DepartmentId = 4, Name = "Департамент 1", OrganizationId = 2 },
                        new { DepartmentId = 5, Name = "Департамент 2", OrganizationId = 2 },
                        new { DepartmentId = 6, Name = "Департамент 1", OrganizationId = 3 }
                    );
                });

            modelBuilder.Entity("EmployeesCatalog.Data.Entities.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepartmentId");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("Patronymic");

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.HasKey("EmployeeId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employees");

                    b.HasData(
                        new { EmployeeId = 1, DepartmentId = 1, Email = "ivanov@test.ru", FirstName = "Иван", Patronymic = "Иванович", Surname = "Иванов" },
                        new { EmployeeId = 2, DepartmentId = 4, Email = "petrov@test.ru", FirstName = "Петр", Patronymic = "Петрович", Surname = "Петров" },
                        new { EmployeeId = 3, DepartmentId = 6, Email = "sidorov@test.ru", FirstName = "Сидор", Patronymic = "Сидорович", Surname = "Сидоров" }
                    );
                });

            modelBuilder.Entity("EmployeesCatalog.Data.Entities.Organization", b =>
                {
                    b.Property<int>("OrganizationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("OrganizationId");

                    b.ToTable("Organizations");

                    b.HasData(
                        new { OrganizationId = 1, Name = "Тестовая организация 1" },
                        new { OrganizationId = 2, Name = "Тестовая организация 2" },
                        new { OrganizationId = 3, Name = "Тестовая организация 3" }
                    );
                });

            modelBuilder.Entity("EmployeesCatalog.Data.Entities.Department", b =>
                {
                    b.HasOne("EmployeesCatalog.Data.Entities.Organization", "Organization")
                        .WithMany("Departments")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmployeesCatalog.Data.Entities.Employee", b =>
                {
                    b.HasOne("EmployeesCatalog.Data.Entities.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
