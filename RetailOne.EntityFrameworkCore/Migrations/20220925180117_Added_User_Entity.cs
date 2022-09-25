using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicationMockup.EntityFrameworkCore.Migrations
{
    public partial class Added_User_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    PIN = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
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
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
