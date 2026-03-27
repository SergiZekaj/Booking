using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHostRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "IsDefault", "Name" },
                values: new object[,]
                {
                    { new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"), "Administrator role", false, "Admin" },
                    { new Guid("a3f1e2d4-5b6c-7a8b-9c0d-1e2f3a4b5c6d"), "Property owner role", false, "Host" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a3f1e2d4-5b6c-7a8b-9c0d-1e2f3a4b5c6d"));
        }
    }
}
