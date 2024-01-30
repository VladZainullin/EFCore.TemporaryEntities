using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sample.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Identification_Name = table.Column<string>(type: "text", nullable: false),
                    Identification_Surname = table.Column<string>(type: "text", nullable: false),
                    Identification_DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Identification_Gender = table.Column<int>(type: "integer", nullable: false),
                    Family_QuantityOfChildren = table.Column<int>(type: "integer", nullable: false),
                    Family_HasPartner = table.Column<bool>(type: "boolean", nullable: false),
                    Work_Salary = table.Column<decimal>(type: "numeric", nullable: false),
                    Work_Address_Country = table.Column<string>(type: "text", nullable: false),
                    Work_Address_Region = table.Column<string>(type: "text", nullable: false),
                    Work_Address_City = table.Column<string>(type: "text", nullable: false),
                    Work_Address_Street = table.Column<string>(type: "text", nullable: false),
                    Work_Address_House = table.Column<string>(type: "text", nullable: false),
                    Address_Country = table.Column<string>(type: "text", nullable: false),
                    Address_Region = table.Column<string>(type: "text", nullable: false),
                    Address_City = table.Column<string>(type: "text", nullable: false),
                    Address_Street = table.Column<string>(type: "text", nullable: false),
                    Address_House = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_Family_HasPartner",
                table: "People",
                column: "Family_HasPartner");

            migrationBuilder.CreateIndex(
                name: "IX_People_Identification_DateOfBirth",
                table: "People",
                column: "Identification_DateOfBirth",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_Identification_Gender",
                table: "People",
                column: "Identification_Gender",
                descending: new bool[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
