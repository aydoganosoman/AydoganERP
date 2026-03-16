using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AydoganERP.Base.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Migration_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EInvoiceIntegrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegrationType = table.Column<int>(type: "integer", nullable: false),
                    Settings = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EInvoiceIntegrations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EInvoiceIntegrations_CompanyId_IntegrationType",
                table: "EInvoiceIntegrations",
                columns: new[] { "CompanyId", "IntegrationType" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EInvoiceIntegrations");
        }
    }
}
