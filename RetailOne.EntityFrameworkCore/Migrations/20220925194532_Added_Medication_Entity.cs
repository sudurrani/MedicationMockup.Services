using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicationMockup.EntityFrameworkCore.Migrations
{
    public partial class Added_Medication_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescriptionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Dosage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medications_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medications_Users_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Medications_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medications_CreatedById",
                table: "Medications",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_DeletedById",
                table: "Medications",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_UpdatedById",
                table: "Medications",
                column: "UpdatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medications");
        }
    }
}
