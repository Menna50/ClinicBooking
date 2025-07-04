using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicBooking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addBioConsultantFeesPhoneToDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "doctors",
                newName: "Bio");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ConsultationFee",
                table: "doctors",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "doctors",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConsultationFee",
                value: 1000m);

            migrationBuilder.UpdateData(
                table: "doctors",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConsultationFee",
                value: 1000m);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PhoneNumber",
                value: "123456789");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PhoneNumber",
                value: "123456789");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PhoneNumber",
                value: "123456789");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PhoneNumber",
                value: "123456789");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5,
                column: "PhoneNumber",
                value: "123456789");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "users");

            migrationBuilder.DropColumn(
                name: "ConsultationFee",
                table: "doctors");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "doctors",
                newName: "Description");
        }
    }
}
