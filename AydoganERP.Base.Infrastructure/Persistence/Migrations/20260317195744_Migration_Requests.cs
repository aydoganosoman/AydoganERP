using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AydoganERP.Base.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Migration_Requests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProviderRequest",
                table: "EInvoiceLogs",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProviderRequest",
                table: "EInvoiceLogs");
        }
    }
}
