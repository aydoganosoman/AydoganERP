using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AydoganERP.Base.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UseCompanyValueObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contact_Fax",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contact_Website",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contact_Fax",
                table: "CustomerBranches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contact_Website",
                table: "CustomerBranches",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contact_Fax",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Contact_Website",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Contact_Fax",
                table: "CustomerBranches");

            migrationBuilder.DropColumn(
                name: "Contact_Website",
                table: "CustomerBranches");
        }
    }
}
