using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicBooking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addIsDeletedToDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "doctors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "doctors",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "doctors",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDeleted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "doctors");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "patients",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "patients",
                keyColumn: "Id",
                keyValue: 1,
                column: "Phone",
                value: "0100000001");

            migrationBuilder.UpdateData(
                table: "patients",
                keyColumn: "Id",
                keyValue: 2,
                column: "Phone",
                value: "0100000002");
        }
    }
}
