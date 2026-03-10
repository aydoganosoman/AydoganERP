using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AydoganERP.Base.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddECommerceIntegration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ECommerceIntegrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegrationType = table.Column<int>(type: "integer", nullable: false),
                    StoreName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IntegrationUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Username = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Credentials = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECommerceIntegrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationDefaults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegrationId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConsiderOrderStatuses = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    OrderStatuses = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    AutoCreateBarcode = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    InvoiceDateType = table.Column<int>(type: "integer", nullable: true),
                    DefaultVatRate = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false, defaultValue: 0m),
                    VatExemptionCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ExportVatExemptionCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ShippingFeeAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    InstallmentFeeAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DefaultCustomerId = table.Column<Guid>(type: "uuid", nullable: true),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: true),
                    CargoCompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    DefaultCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    EInvoiceSeriesId = table.Column<Guid>(type: "uuid", nullable: true),
                    EArchiveSeriesId = table.Column<Guid>(type: "uuid", nullable: true),
                    OrderFilterDaysBefore = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    LastSyncTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastOrderFilterDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastSyncStatus = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationDefaults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntegrationDefaults_ECommerceIntegrations_IntegrationId",
                        column: x => x.IntegrationId,
                        principalTable: "ECommerceIntegrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ECommerceIntegrations_CompanyId_IntegrationType",
                table: "ECommerceIntegrations",
                columns: new[] { "CompanyId", "IntegrationType" });

            migrationBuilder.CreateIndex(
                name: "IX_IntegrationDefaults_IntegrationId",
                table: "IntegrationDefaults",
                column: "IntegrationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntegrationDefaults");

            migrationBuilder.DropTable(
                name: "ECommerceIntegrations");
        }
    }
}
