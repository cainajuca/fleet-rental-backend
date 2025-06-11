using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fleet.Infra.Database.Migrations
{
    /// <inheritdoc />
    public partial class ConvertCnhTypeToIntAndAddUniqueIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cnh_type",
                table: "deliveryman");

            migrationBuilder.AddColumn<int>(
                name: "cnh_type",
                table: "deliveryman",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_vehicle_identifier",
                table: "vehicle",
                column: "identifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_app_user_username",
                table: "app_user",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_vehicle_identifier",
                table: "vehicle");

            migrationBuilder.DropIndex(
                name: "ix_app_user_username",
                table: "app_user");

            migrationBuilder.DropColumn(
                name: "cnh_type",
                table: "deliveryman");

            migrationBuilder.AddColumn<string>(
                name: "cnh_type",
                table: "deliveryman",
                type: "character varying(5)",
                maxLength: 5,
                nullable: false);
        }
    }
}
