using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clgproject.Migrations
{
    /// <inheritdoc />
    public partial class invent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupplierHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainsuppId = table.Column<int>(type: "int", nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierHistories_MainSuppliers_MainsuppId",
                        column: x => x.MainsuppId,
                        principalTable: "MainSuppliers",
                        principalColumn: "mainsupp_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierHistories_MainsuppId",
                table: "SupplierHistories",
                column: "MainsuppId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierHistories");
        }
    }
}
