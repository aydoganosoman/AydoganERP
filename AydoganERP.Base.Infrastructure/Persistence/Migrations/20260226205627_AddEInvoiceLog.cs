using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AydoganERP.Base.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEInvoiceLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EInvoiceLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    EInvoiceUUID = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ResponseAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ProviderResponse = table.Column<string>(type: "text", nullable: true),
                    ErrorMessage = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    ProviderName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UblXmlContent = table.Column<string>(type: "text", nullable: true),
                    PdfPath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EInvoiceLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EInvoiceLogs_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EInvoiceLogs_EInvoiceUUID",
                table: "EInvoiceLogs",
                column: "EInvoiceUUID");

            migrationBuilder.CreateIndex(
                name: "IX_EInvoiceLogs_InvoiceId",
                table: "EInvoiceLogs",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_EInvoiceLogs_Status",
                table: "EInvoiceLogs",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EInvoiceLogs");
        }
    }
}
