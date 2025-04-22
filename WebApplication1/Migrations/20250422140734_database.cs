using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoServiceApi.Migrations
{
    /// <inheritdoc />
    public partial class database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "ClosingTime",
                table: "AutoServices",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningTime",
                table: "AutoServices",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "AutoServices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClosingTime", "OpeningTime" },
                values: new object[] { new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "ServicesOffers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 22, 14, 7, 33, 605, DateTimeKind.Utc).AddTicks(5227));

            migrationBuilder.UpdateData(
                table: "ServicesOffers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 22, 14, 7, 33, 605, DateTimeKind.Utc).AddTicks(5806));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingTime",
                table: "AutoServices");

            migrationBuilder.DropColumn(
                name: "OpeningTime",
                table: "AutoServices");

            migrationBuilder.UpdateData(
                table: "ServicesOffers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 22, 13, 27, 29, 89, DateTimeKind.Utc).AddTicks(1194));

            migrationBuilder.UpdateData(
                table: "ServicesOffers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 4, 22, 13, 27, 29, 89, DateTimeKind.Utc).AddTicks(1755));
        }
    }
}
