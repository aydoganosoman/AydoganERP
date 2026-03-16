using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AydoganERP.Base.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceFormEnhancements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EInvoiceScenario",
                table: "Invoices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceSerial",
                table: "Invoices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "InvoiceSubDiscount",
                table: "Invoices",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "InvoiceTime",
                table: "Invoices",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PayableAmount",
                table: "Invoices",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PostboxAlias",
                table: "Invoices",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReplacesInvoiceRef",
                table: "Invoices",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "RoundingAmount",
                table: "Invoices",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SeriesPrefix",
                table: "Invoices",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GtipCode",
                table: "InvoiceLines",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LineType",
                table: "InvoiceLines",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VatStatus",
                table: "InvoiceLines",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AttachmentType = table.Column<int>(type: "integer", nullable: false),
                    RelatedEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ContentType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    NoteText = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceNotes_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceOkcInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    FisNo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    FisDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FisTime = table.Column<TimeSpan>(type: "interval", nullable: true),
                    FisType = table.Column<int>(type: "integer", nullable: true),
                    ZReportNo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OkcSerialNo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceOkcInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceOkcInfos_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceOrderInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OrderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaybillNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    WaybillDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DocumentPath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DocumentName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceOrderInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceOrderInfos_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePartyNumbers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsBuyer = table.Column<bool>(type: "boolean", nullable: false),
                    NumberType = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePartyNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoicePartyNumbers_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePaymentTerms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    PenaltyRate = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    PenaltyAmount = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePaymentTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoicePaymentTerms_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_EInvoiceUUID",
                table: "Invoices",
                column: "EInvoiceUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_AttachmentType",
                table: "Documents",
                column: "AttachmentType");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_AttachmentType_RelatedEntityId",
                table: "Documents",
                columns: new[] { "AttachmentType", "RelatedEntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_RelatedEntityId",
                table: "Documents",
                column: "RelatedEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceNotes_InvoiceId",
                table: "InvoiceNotes",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOkcInfos_InvoiceId",
                table: "InvoiceOkcInfos",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOrderInfos_InvoiceId",
                table: "InvoiceOrderInfos",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOrderInfos_OrderNumber",
                table: "InvoiceOrderInfos",
                column: "OrderNumber");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOrderInfos_WaybillNumber",
                table: "InvoiceOrderInfos",
                column: "WaybillNumber");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePartyNumbers_InvoiceId",
                table: "InvoicePartyNumbers",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePartyNumbers_InvoiceId_IsBuyer_NumberType",
                table: "InvoicePartyNumbers",
                columns: new[] { "InvoiceId", "IsBuyer", "NumberType" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePaymentTerms_InvoiceId",
                table: "InvoicePaymentTerms",
                column: "InvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "InvoiceNotes");

            migrationBuilder.DropTable(
                name: "InvoiceOkcInfos");

            migrationBuilder.DropTable(
                name: "InvoiceOrderInfos");

            migrationBuilder.DropTable(
                name: "InvoicePartyNumbers");

            migrationBuilder.DropTable(
                name: "InvoicePaymentTerms");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_EInvoiceUUID",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "EInvoiceScenario",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceSerial",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceSubDiscount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceTime",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PayableAmount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PostboxAlias",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ReplacesInvoiceRef",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "RoundingAmount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "SeriesPrefix",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "GtipCode",
                table: "InvoiceLines");

            migrationBuilder.DropColumn(
                name: "LineType",
                table: "InvoiceLines");

            migrationBuilder.DropColumn(
                name: "VatStatus",
                table: "InvoiceLines");
        }
    }
}
