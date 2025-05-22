using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Contact_ContactId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contact",
                table: "Contact");

            migrationBuilder.RenameTable(
                name: "Contact",
                newName: "Contacts");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_PhoneNumber",
                table: "Contacts",
                newName: "IX_Contacts_PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_PassportSeries",
                table: "Contacts",
                newName: "IX_Contacts_PassportSeries");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_LastName",
                table: "Contacts",
                newName: "IX_Contacts_LastName");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_FirstName",
                table: "Contacts",
                newName: "IX_Contacts_FirstName");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_Email",
                table: "Contacts",
                newName: "IX_Contacts_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_Address",
                table: "Contacts",
                newName: "IX_Contacts_Address");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contacts",
                table: "Contacts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Contacts_ContactId",
                table: "Users",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Contacts_ContactId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contacts",
                table: "Contacts");

            migrationBuilder.RenameTable(
                name: "Contacts",
                newName: "Contact");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_PhoneNumber",
                table: "Contact",
                newName: "IX_Contact_PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_PassportSeries",
                table: "Contact",
                newName: "IX_Contact_PassportSeries");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_LastName",
                table: "Contact",
                newName: "IX_Contact_LastName");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_FirstName",
                table: "Contact",
                newName: "IX_Contact_FirstName");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_Email",
                table: "Contact",
                newName: "IX_Contact_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_Address",
                table: "Contact",
                newName: "IX_Contact_Address");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contact",
                table: "Contact",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Contact_ContactId",
                table: "Users",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id");
        }
    }
}
