using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTAuthentication.Migrations
{
    /// <inheritdoc />
    public partial class RolecolumnaddedinUserCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "UserCredentaials",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "UserCredentaials");
        }
    }
}
