using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediSyncHub.Modules.AppointmentBookingModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateAppointmentBookingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "booking");

            migrationBuilder.CreateTable(
                name: "Appointments",
                schema: "booking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SlotId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientName = table.Column<string>(type: "text", nullable: false),
                    ReservedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                schema: "booking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsReserved = table.Column<bool>(type: "boolean", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments",
                schema: "booking");

            migrationBuilder.DropTable(
                name: "Slots",
                schema: "booking");
        }
    }
}
