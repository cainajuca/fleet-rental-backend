using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fleet.Infra.Database.Migrations
{
    /// <inheritdoc />
    public partial class Refactor_AppUserAndDeliveryman : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "birth_date",
                table: "deliveryman");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "deliveryman");

            migrationBuilder.DropColumn(
                name: "name",
                table: "deliveryman");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "app_user",
                newName: "name");

            migrationBuilder.AddColumn<DateTime>(
                name: "birth_date",
                table: "app_user",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "app_user",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "birth_date",
                table: "app_user");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "app_user");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "app_user",
                newName: "email");

            migrationBuilder.AddColumn<DateTime>(
                name: "birth_date",
                table: "deliveryman",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "deliveryman",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "deliveryman",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
