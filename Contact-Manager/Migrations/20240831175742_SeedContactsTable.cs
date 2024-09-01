using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contact_Manager.Migrations
{
    /// <inheritdoc />
    public partial class SeedContactsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Birthday", "Married", "Name", "Phone", "Salary" },
                values: new object[] { 1, new DateTime(2005, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Steven", "0938265605", 1000.0m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
