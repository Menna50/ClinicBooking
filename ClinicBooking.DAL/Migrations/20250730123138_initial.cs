using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClinicBooking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "specialities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecialtyId = table.Column<int>(type: "int", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ConsultationFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_doctors_specialities_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_doctors_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_patients_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "availabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    SlotDurationMinutes = table.Column<int>(type: "int", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EffectiveTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_availabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_availabilities_doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_appointments_doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_appointments_patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "specialities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Cardiology" },
                    { 2, null, "Dermatology" },
                    { 3, null, "Pediatrics" },
                    { 4, null, "Neurology" },
                    { 5, null, "Orthopedics" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "PhoneNumber", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, "admin@gmail.com", new byte[0], new byte[0], "123456789", "Admin", "admin" },
                    { 2, "doctor1@gmail.com", new byte[0], new byte[0], "123456789", "Doctor", "doctor1" },
                    { 3, "doctor2@gmail.com", new byte[0], new byte[0], "123456789", "Doctor", "doctor2" },
                    { 4, "patient1@gmail.com", new byte[0], new byte[0], "123456789", "Patient", "patient1" },
                    { 5, "patient2@gmail.com", new byte[0], new byte[0], "123456789", "Patient", "patient2" }
                });

            migrationBuilder.InsertData(
                table: "doctors",
                columns: new[] { "Id", "Bio", "ConsultationFee", "IsDeleted", "Name", "SpecialtyId", "UserId" },
                values: new object[,]
                {
                    { 1, "Senior Doctor", 1000m, false, "doctor1", 1, 2 },
                    { 2, "Specialist", 1000m, false, "doctor2", 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "patients",
                columns: new[] { "Id", "Age", "FName", "Gender", "LName", "UserId" },
                values: new object[,]
                {
                    { 1, 30, "Ali", "Male", "Hassan", 4 },
                    { 2, 25, "Sara", "Female", "Youssef", 5 }
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
                columns: new[] { "Id", "Day", "DoctorId", "EffectiveFrom", "EffectiveTo", "EndTime", "SlotDurationMinutes", "StartTime" },
                values: new object[,]
                {
                    { 1, "Monday", 1, null, null, new TimeOnly(12, 0, 0), 0, new TimeOnly(9, 0, 0) },
                    { 2, "Wednesday", 1, null, null, new TimeOnly(13, 0, 0), 0, new TimeOnly(10, 0, 0) },
                    { 3, "Tuesday", 2, null, null, new TimeOnly(14, 0, 0), 0, new TimeOnly(11, 0, 0) },
                    { 4, "Thursday", 2, null, null, new TimeOnly(12, 0, 0), 0, new TimeOnly(9, 0, 0) },
                    { 5, "Friday", 1, null, null, new TimeOnly(12, 30, 0), 0, new TimeOnly(10, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_DoctorId",
                table: "appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_PatientId",
                table: "appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_availabilities_DoctorId",
                table: "availabilities",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_doctors_SpecialtyId",
                table: "doctors",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_doctors_UserId",
                table: "doctors",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_patients_UserId",
                table: "patients",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_UserName",
                table: "users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "availabilities");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "doctors");

            migrationBuilder.DropTable(
                name: "specialities");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
