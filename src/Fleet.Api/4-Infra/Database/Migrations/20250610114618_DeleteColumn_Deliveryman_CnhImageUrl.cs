using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fleet.Infra.Database.Migrations
{
    /// <inheritdoc />
    public partial class DeleteColumn_Deliveryman_CnhImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cnh_image_url",
                table: "deliveryman");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cnh_image_url",
                table: "deliveryman",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
