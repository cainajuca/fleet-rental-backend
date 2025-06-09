using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fleet.Infra.Database.Migrations
{
    /// <inheritdoc />
    public partial class ApplySnakeCaseNamingConvention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveryman_AppUser_AppUserId",
                table: "Deliveryman");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Deliveryman_DeliverymanId",
                table: "Rental");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Vehicle_VehicleId",
                table: "Rental");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rental",
                table: "Rental");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deliveryman",
                table: "Deliveryman");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationMessage",
                table: "NotificationMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUser",
                table: "AppUser");

            migrationBuilder.RenameTable(
                name: "Vehicle",
                newName: "vehicle");

            migrationBuilder.RenameTable(
                name: "Rental",
                newName: "rental");

            migrationBuilder.RenameTable(
                name: "Deliveryman",
                newName: "deliveryman");

            migrationBuilder.RenameTable(
                name: "NotificationMessage",
                newName: "notification_message");

            migrationBuilder.RenameTable(
                name: "AppUser",
                newName: "app_user");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "vehicle",
                newName: "year");

            migrationBuilder.RenameColumn(
                name: "Model",
                table: "vehicle",
                newName: "model");

            migrationBuilder.RenameColumn(
                name: "Identifier",
                table: "vehicle",
                newName: "identifier");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "vehicle",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LicensePlate",
                table: "vehicle",
                newName: "license_plate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "vehicle",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Penalty",
                table: "rental",
                newName: "penalty");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "rental",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "rental",
                newName: "vehicle_id");

            migrationBuilder.RenameColumn(
                name: "TotalCost",
                table: "rental",
                newName: "total_cost");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "rental",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "ReturnedAt",
                table: "rental",
                newName: "returned_at");

            migrationBuilder.RenameColumn(
                name: "PlanDays",
                table: "rental",
                newName: "plan_days");

            migrationBuilder.RenameColumn(
                name: "ExpectedEndDate",
                table: "rental",
                newName: "expected_end_date");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "rental",
                newName: "end_date");

            migrationBuilder.RenameColumn(
                name: "DeliverymanId",
                table: "rental",
                newName: "deliveryman_id");

            migrationBuilder.RenameColumn(
                name: "DailyRate",
                table: "rental",
                newName: "daily_rate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "rental",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_VehicleId",
                table: "rental",
                newName: "ix_rental_vehicle_id");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_DeliverymanId",
                table: "rental",
                newName: "ix_rental_deliveryman_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "deliveryman",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Cnpj",
                table: "deliveryman",
                newName: "cnpj");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "deliveryman",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "deliveryman",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CnhType",
                table: "deliveryman",
                newName: "cnh_type");

            migrationBuilder.RenameColumn(
                name: "CnhNumber",
                table: "deliveryman",
                newName: "cnh_number");

            migrationBuilder.RenameColumn(
                name: "CnhImageUrl",
                table: "deliveryman",
                newName: "cnh_image_url");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "deliveryman",
                newName: "birth_date");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "deliveryman",
                newName: "app_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveryman_Cnpj",
                table: "deliveryman",
                newName: "ix_deliveryman_cnpj");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveryman_CnhNumber",
                table: "deliveryman",
                newName: "ix_deliveryman_cnh_number");

            migrationBuilder.RenameIndex(
                name: "IX_Deliveryman_AppUserId",
                table: "deliveryman",
                newName: "ix_deliveryman_app_user_id");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "notification_message",
                newName: "message");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "notification_message",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ReceivedAt",
                table: "notification_message",
                newName: "received_at");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "app_user",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "app_user",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "app_user",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "app_user",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "app_user",
                newName: "password_hash");

            migrationBuilder.AddPrimaryKey(
                name: "pk_vehicle",
                table: "vehicle",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_rental",
                table: "rental",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_deliveryman",
                table: "deliveryman",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_notification_message",
                table: "notification_message",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_app_user",
                table: "app_user",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_deliveryman_app_user_app_user_id",
                table: "deliveryman",
                column: "app_user_id",
                principalTable: "app_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_rental_deliveryman_deliveryman_id",
                table: "rental",
                column: "deliveryman_id",
                principalTable: "deliveryman",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_rental_vehicle_vehicle_id",
                table: "rental",
                column: "vehicle_id",
                principalTable: "vehicle",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_deliveryman_app_user_app_user_id",
                table: "deliveryman");

            migrationBuilder.DropForeignKey(
                name: "fk_rental_deliveryman_deliveryman_id",
                table: "rental");

            migrationBuilder.DropForeignKey(
                name: "fk_rental_vehicle_vehicle_id",
                table: "rental");

            migrationBuilder.DropPrimaryKey(
                name: "pk_vehicle",
                table: "vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "pk_rental",
                table: "rental");

            migrationBuilder.DropPrimaryKey(
                name: "pk_deliveryman",
                table: "deliveryman");

            migrationBuilder.DropPrimaryKey(
                name: "pk_notification_message",
                table: "notification_message");

            migrationBuilder.DropPrimaryKey(
                name: "pk_app_user",
                table: "app_user");

            migrationBuilder.RenameTable(
                name: "vehicle",
                newName: "Vehicle");

            migrationBuilder.RenameTable(
                name: "rental",
                newName: "Rental");

            migrationBuilder.RenameTable(
                name: "deliveryman",
                newName: "Deliveryman");

            migrationBuilder.RenameTable(
                name: "notification_message",
                newName: "NotificationMessage");

            migrationBuilder.RenameTable(
                name: "app_user",
                newName: "AppUser");

            migrationBuilder.RenameColumn(
                name: "year",
                table: "Vehicle",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "model",
                table: "Vehicle",
                newName: "Model");

            migrationBuilder.RenameColumn(
                name: "identifier",
                table: "Vehicle",
                newName: "Identifier");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Vehicle",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "license_plate",
                table: "Vehicle",
                newName: "LicensePlate");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Vehicle",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "penalty",
                table: "Rental",
                newName: "Penalty");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Rental",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "vehicle_id",
                table: "Rental",
                newName: "VehicleId");

            migrationBuilder.RenameColumn(
                name: "total_cost",
                table: "Rental",
                newName: "TotalCost");

            migrationBuilder.RenameColumn(
                name: "start_date",
                table: "Rental",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "returned_at",
                table: "Rental",
                newName: "ReturnedAt");

            migrationBuilder.RenameColumn(
                name: "plan_days",
                table: "Rental",
                newName: "PlanDays");

            migrationBuilder.RenameColumn(
                name: "expected_end_date",
                table: "Rental",
                newName: "ExpectedEndDate");

            migrationBuilder.RenameColumn(
                name: "end_date",
                table: "Rental",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "deliveryman_id",
                table: "Rental",
                newName: "DeliverymanId");

            migrationBuilder.RenameColumn(
                name: "daily_rate",
                table: "Rental",
                newName: "DailyRate");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Rental",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_rental_vehicle_id",
                table: "Rental",
                newName: "IX_Rental_VehicleId");

            migrationBuilder.RenameIndex(
                name: "ix_rental_deliveryman_id",
                table: "Rental",
                newName: "IX_Rental_DeliverymanId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Deliveryman",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "cnpj",
                table: "Deliveryman",
                newName: "Cnpj");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Deliveryman",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Deliveryman",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "cnh_type",
                table: "Deliveryman",
                newName: "CnhType");

            migrationBuilder.RenameColumn(
                name: "cnh_number",
                table: "Deliveryman",
                newName: "CnhNumber");

            migrationBuilder.RenameColumn(
                name: "cnh_image_url",
                table: "Deliveryman",
                newName: "CnhImageUrl");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                table: "Deliveryman",
                newName: "BirthDate");

            migrationBuilder.RenameColumn(
                name: "app_user_id",
                table: "Deliveryman",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "ix_deliveryman_cnpj",
                table: "Deliveryman",
                newName: "IX_Deliveryman_Cnpj");

            migrationBuilder.RenameIndex(
                name: "ix_deliveryman_cnh_number",
                table: "Deliveryman",
                newName: "IX_Deliveryman_CnhNumber");

            migrationBuilder.RenameIndex(
                name: "ix_deliveryman_app_user_id",
                table: "Deliveryman",
                newName: "IX_Deliveryman_AppUserId");

            migrationBuilder.RenameColumn(
                name: "message",
                table: "NotificationMessage",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "NotificationMessage",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "received_at",
                table: "NotificationMessage",
                newName: "ReceivedAt");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "AppUser",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "AppUser",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "AppUser",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AppUser",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "AppUser",
                newName: "PasswordHash");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rental",
                table: "Rental",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deliveryman",
                table: "Deliveryman",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationMessage",
                table: "NotificationMessage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUser",
                table: "AppUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveryman_AppUser_AppUserId",
                table: "Deliveryman",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Deliveryman_DeliverymanId",
                table: "Rental",
                column: "DeliverymanId",
                principalTable: "Deliveryman",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Vehicle_VehicleId",
                table: "Rental",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
