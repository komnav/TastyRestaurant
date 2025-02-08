using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Contacts_ContactId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Warenches_Contacts_ContactId",
                table: "Warenches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warenches",
                table: "Warenches");

            migrationBuilder.RenameTable(
                name: "Warenches",
                newName: "Waiters");

            migrationBuilder.RenameIndex(
                name: "IX_Warenches_ContactId",
                table: "Waiters",
                newName: "IX_Waiters_ContactId");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "Customers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Waiters",
                table: "Waiters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Contacts_ContactId",
                table: "Customers",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Waiters_Contacts_ContactId",
                table: "Waiters",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Contacts_ContactId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Waiters_Contacts_ContactId",
                table: "Waiters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Waiters",
                table: "Waiters");

            migrationBuilder.RenameTable(
                name: "Waiters",
                newName: "Warenches");

            migrationBuilder.RenameIndex(
                name: "IX_Waiters_ContactId",
                table: "Warenches",
                newName: "IX_Warenches_ContactId");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "Customers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warenches",
                table: "Warenches",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Contacts_ContactId",
                table: "Customers",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Warenches_Contacts_ContactId",
                table: "Warenches",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");
        }
    }
}
