using Infrastructure.Enums;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;
using Model = Infrastructure.Models.Users;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { nameof(Model.Id), nameof(Model.Login), nameof(Model.Password), nameof(Model.CreatedDate), nameof(Model.UsersGroupId), nameof(Model.UsersStateId) },
                columnTypes: new[] {"integer", "text", "text", "timestamp with time zone", "integer", "integer" },
                values: new object[,] { { 1, "admin", PasswordHelper.EncodePasswordToBase64("admin"), DateTime.UtcNow, (int)UserGroupCode.Admin, (int)UserStateCode.Active } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "User",
                new[] {
                    nameof(Model.Id),
                },
                new object[] {
                    1,
                }
            );
        }

    }
}