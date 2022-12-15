using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KontaktAPI.Migrations
{
    public partial class UserAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Categories = table.Column<int>(type: "INTEGER", nullable: false),
                    SubCategory = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<int>(type: "INTEGER", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Categories", "DateOfBirth", "Email", "Name", "Password", "Phone", "SubCategory", "Surname" },
                values: new object[] { 1, 1, new DateTime(2022, 12, 14, 17, 50, 52, 512, DateTimeKind.Local).AddTicks(1513), "j.kowalski@gmail.com", "Jan", "123456", 123456789, "", "Kowalski" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Categories", "DateOfBirth", "Email", "Name", "Password", "Phone", "SubCategory", "Surname" },
                values: new object[] { 2, 0, new DateTime(2022, 12, 14, 17, 50, 52, 512, DateTimeKind.Local).AddTicks(1553), "s.Nowak@gmail.com", "Stefan", "123456", 123456782, "Szef", "Nowak" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Categories", "DateOfBirth", "Email", "Name", "Password", "Phone", "SubCategory", "Surname" },
                values: new object[] { 3, 0, new DateTime(2022, 12, 14, 17, 50, 52, 512, DateTimeKind.Local).AddTicks(1557), "j.Kowalska@gmail.com", "Janina", "123456", 123456783, "Klient", "Kowalska" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Categories", "DateOfBirth", "Email", "Name", "Password", "Phone", "SubCategory", "Surname" },
                values: new object[] { 4, 1, new DateTime(2022, 12, 14, 17, 50, 52, 512, DateTimeKind.Local).AddTicks(1559), "m.kowalski@gmail.com", "Marek", "123456", 123456784, "", "Kowalski" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Categories", "DateOfBirth", "Email", "Name", "Password", "Phone", "SubCategory", "Surname" },
                values: new object[] { 5, 1, new DateTime(2022, 12, 14, 17, 50, 52, 512, DateTimeKind.Local).AddTicks(1560), "a.Kowalska@gmail.com", "Agnieszka", "123456", 123456785, "", "Kowalska" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { 1, "User" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
