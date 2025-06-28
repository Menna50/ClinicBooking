using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClinicBooking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class deleteSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "appointments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "appointments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "appointments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "appointments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "appointments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "availabilities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "availabilities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "availabilities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "availabilities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "availabilities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "doctors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "doctors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "patients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "patients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new byte[0], new byte[0], "Admin", "admin" },
                    { 2, new byte[0], new byte[0], "Doctor", "doctor1" },
                    { 3, new byte[0], new byte[0], "Doctor", "doctor2" },
                    { 4, new byte[0], new byte[0], "Patient", "patient1" },
                    { 5, new byte[0], new byte[0], "Patient", "patient2" }
                });

            migrationBuilder.InsertData(
                table: "doctors",
                columns: new[] { "Id", "Description", "Name", "Specialty", "UserId" },
                values: new object[,]
                {
                    { 1, "Senior Doctor", "Dr. Ahmed", "Cardiology", 2 },
                    { 2, "Specialist", "Dr. Mona", "Dermatology", 3 }
                });

            migrationBuilder.InsertData(
                table: "patients",
                columns: new[] { "Id", "Age", "FName", "Gender", "LName", "Phone", "UserId" },
                values: new object[,]
                {
                    { 1, 30, "Ali", "Male", "Hassan", "0100000001", 4 },
                    { 2, 25, "Sara", "Female", "Youssef", "0100000002", 5 }
                });

            migrationBuilder.InsertData(
                table: "appointments",
                columns: new[] { "Id", "AppointmentDate", "DoctorId", "PatientId", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Scheduled" },
                    { 2, new DateTime(2025, 7, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, "Completed" },
                    { 3, new DateTime(2025, 7, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, "Cancelled" },
                    { 4, new DateTime(2025, 7, 4, 9, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, "Scheduled" },
                    { 5, new DateTime(2025, 7, 5, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "NoShow" }
                });

            migrationBuilder.InsertData(
                table: "availabilities",
                columns: new[] { "Id", "Day", "DoctorId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { 1, "Monday", 1, new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0) },
                    { 2, "Wednesday", 1, new TimeSpan(0, 13, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0) },
                    { 3, "Tuesday", 2, new TimeSpan(0, 14, 0, 0, 0), new TimeSpan(0, 11, 0, 0, 0) },
                    { 4, "Thursday", 2, new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0) },
                    { 5, "Friday", 1, new TimeSpan(0, 12, 30, 0, 0), new TimeSpan(0, 10, 0, 0, 0) }
                });
        }
    }
}
