using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDefaultInformationBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Group",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Group",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Group",
                columns: new[] { "Id", "Code", "Description" },
                values: new object[,]
                {
                    { 1, "Admin", "Aдминистратор" },
                    { 2, "User", "Пользователь" }
                });

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "Id", "Code", "Description" },
                values: new object[,]
                {
                    { 1, "Active", "Активный пользователей" },
                    { 2, "Blocked", "Заблокированый пользователь" }
                });
        }
    }
}
