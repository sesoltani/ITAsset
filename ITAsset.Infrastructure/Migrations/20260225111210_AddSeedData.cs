using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ITAsset.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DeviceTypes",
                columns: new[] { "Id", "CodePerfix", "Name" },
                values: new object[,]
                {
                    { 1, "PC", "کامپیوتر رومیزی" },
                    { 2, "LT", "لپ تاپ" },
                    { 3, "SW", "سوییچ" },
                    { 4, "PR", "پرینتر" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreatedAt", "Department", "Email", "EmployeeCode", "FirstName", "IsActive", "LastName", "PhoneNumber", "Position" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 25, 11, 12, 7, 789, DateTimeKind.Utc).AddTicks(8553), "فناوری اطلاعات", "ali@company.com", "EMP001", "علی", true, "محمدی", null, "کارشناس شبکه" },
                    { 2, new DateTime(2026, 2, 25, 11, 12, 7, 790, DateTimeKind.Utc).AddTicks(1128), "مالی", "maryam@company.com", "EMP002", "مریم", true, "احمدی", null, "کارشناس مالی" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Name", "ParentLocationId" },
                values: new object[] { 1, "انبار مرکزی", null });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "DeviceTypeId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Dell" },
                    { 2, 1, "HP" },
                    { 3, 3, "Cisco" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Name", "ParentLocationId" },
                values: new object[] { 2, "طبقه اول - اتاق سرور", 1 });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "BrandId", "Name", "Specifications" },
                values: new object[,]
                {
                    { 1, 1, "Optiplex 3080", null },
                    { 2, 2, "EliteBook 840", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DeviceTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DeviceTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DeviceTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DeviceTypes",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
