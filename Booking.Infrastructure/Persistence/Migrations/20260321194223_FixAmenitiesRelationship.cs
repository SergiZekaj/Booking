using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAmenitiesRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyAmenities_Properties_PropertyEntityId",
                table: "PropertyAmenities");

            migrationBuilder.DropIndex(
                name: "IX_PropertyAmenities_PropertyEntityId",
                table: "PropertyAmenities");

            migrationBuilder.DropColumn(
                name: "PropertyEntityId",
                table: "PropertyAmenities");

            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "Amenities",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Amenities",
                newName: "Guid");

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyEntityId",
                table: "PropertyAmenities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAmenities_PropertyEntityId",
                table: "PropertyAmenities",
                column: "PropertyEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyAmenities_Properties_PropertyEntityId",
                table: "PropertyAmenities",
                column: "PropertyEntityId",
                principalTable: "Properties",
                principalColumn: "Id");
        }
    }
}
