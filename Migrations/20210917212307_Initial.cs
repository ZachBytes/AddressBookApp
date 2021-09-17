using Microsoft.EntityFrameworkCore.Migrations;

namespace AddressBookApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactID);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactID = table.Column<int>(type: "int", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK_Addresses_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "ContactID", "FirstName", "LastName" },
                values: new object[] { 1, "TestFirst", "TestLast" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "ContactID", "FirstName", "LastName" },
                values: new object[] { 2, "2ndTestFirst", "2ndTestLast" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressID", "City", "ContactID", "PostalCode", "State", "StreetAddress" },
                values: new object[] { 1, "Springfield", 1, "11111", "New Jersey", "1 Any Street" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressID", "City", "ContactID", "PostalCode", "State", "StreetAddress" },
                values: new object[] { 2, "Paramus", 1, "99999", "New Jersey", "2 Some Street" });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ContactID",
                table: "Addresses",
                column: "ContactID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
