using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusRoAppointmnet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Status",
                schema: "booking",
                table: "Appointments",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SlotId",
                schema: "booking",
                table: "Appointments",
                column: "SlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Slots_SlotId",
                schema: "booking",
                table: "Appointments",
                column: "SlotId",
                principalSchema: "booking",
                principalTable: "Slots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Slots_SlotId",
                schema: "booking",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_SlotId",
                schema: "booking",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "booking",
                table: "Appointments");
        }
    }
}
