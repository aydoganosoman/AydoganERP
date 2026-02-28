using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AydoganERP.Base.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Migration_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VatRate",
                table: "Products",
                newName: "SaleUnitPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "PurchaseUnitPrice",
                table: "Products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PurchaseUnitPriceCurrency",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "PurchaseUnitPriceVatInculde",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "PurchaseVatRate",
                table: "Products",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "SaleUnitPriceCurrency",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "SaleUnitPriceVatInculde",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "SaleVatRate",
                table: "Products",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseUnitPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PurchaseUnitPriceCurrency",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PurchaseUnitPriceVatInculde",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PurchaseVatRate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SaleUnitPriceCurrency",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SaleUnitPriceVatInculde",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SaleVatRate",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SaleUnitPrice",
                table: "Products",
                newName: "VatRate");
        }
    }
}
