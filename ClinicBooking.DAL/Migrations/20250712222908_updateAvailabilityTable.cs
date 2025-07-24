using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicBooking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateAvailabilityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveFrom",
                table: "availabilities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveTo",
                table: "availabilities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SlotDurationMinutes",
                table: "availabilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "availabilities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EffectiveFrom", "EffectiveTo", "SlotDurationMinutes" },
                values: new object[] { null, null, 0 });

            migrationBuilder.UpdateData(
                table: "availabilities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EffectiveFrom", "EffectiveTo", "SlotDurationMinutes" },
                values: new object[] { null, null, 0 });

            migrationBuilder.UpdateData(
                table: "availabilities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EffectiveFrom", "EffectiveTo", "SlotDurationMinutes" },
                values: new object[] { null, null, 0 });

            migrationBuilder.UpdateData(
                table: "availabilities",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EffectiveFrom", "EffectiveTo", "SlotDurationMinutes" },
                values: new object[] { null, null, 0 });

            migrationBuilder.UpdateData(
                table: "availabilities",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EffectiveFrom", "EffectiveTo", "SlotDurationMinutes" },
                values: new object[] { null, null, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EffectiveFrom",
                table: "availabilities");

            migrationBuilder.DropColumn(
                name: "EffectiveTo",
                table: "availabilities");

            migrationBuilder.DropColumn(
                name: "SlotDurationMinutes",
                table: "availabilities");
        }
    }
}
