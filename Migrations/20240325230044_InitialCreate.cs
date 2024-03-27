using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace storageUnitAPi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Spells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    School = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    CastTime = table.Column<int>(type: "int", nullable: false),
                    Components = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Range = table.Column<int>(type: "int", nullable: false),
                    Effect = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SavingThrow = table.Column<bool>(type: "bit", nullable: false),
                    SpellResistance = table.Column<bool>(type: "bit", nullable: false),
                    Known = table.Column<bool>(type: "bit", nullable: false),
                    Prepared = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spells", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StorageUnitId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorageUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationStartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CurrentOwnerId = table.Column<int>(type: "int", nullable: true),
                    PreviousOwnersIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorageUnit_Customers_CurrentOwnerId",
                        column: x => x.CurrentOwnerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_StorageUnitId",
                table: "Customers",
                column: "StorageUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageUnit_CurrentOwnerId",
                table: "StorageUnit",
                column: "CurrentOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_StorageUnit_StorageUnitId",
                table: "Customers",
                column: "StorageUnitId",
                principalTable: "StorageUnit",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_StorageUnit_StorageUnitId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "Spells");

            migrationBuilder.DropTable(
                name: "StorageUnit");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
