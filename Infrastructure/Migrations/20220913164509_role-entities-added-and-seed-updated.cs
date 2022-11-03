using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class roleentitiesaddedandseedupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Corporate",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HeadQuarterName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HeadQuarterContact = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corporate", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserCandidateProfile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_UserID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCandidateProfile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserCandidateProfile_User_fk_UserID",
                        column: x => x.fk_UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCorporateProfile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_CorporateID = table.Column<int>(type: "int", nullable: false),
                    fk_UserID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCorporateProfile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserCorporateProfile_Corporate_fk_CorporateID",
                        column: x => x.fk_CorporateID,
                        principalTable: "Corporate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCorporateProfile_User_fk_UserID",
                        column: x => x.fk_UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumn: "ID",
                keyValue: 1,
                column: "CanViewUsers",
                value: true);

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "ID", "CanAddUser", "CanDeleteUser", "CanEditUser", "CanViewUsers", "CreatedAt", "CreatedBy", "IsActive", "IsDeleted", "ModifiedAt", "ModifiedBy", "RoleName" },
                values: new object[,]
                {
                    { 2, false, false, false, true, null, null, true, false, null, null, "CorporateUser" },
                    { 3, false, false, false, true, null, null, true, false, null, null, "CandidateUser" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCandidateProfile_fk_UserID",
                table: "UserCandidateProfile",
                column: "fk_UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCorporateProfile_fk_CorporateID",
                table: "UserCorporateProfile",
                column: "fk_CorporateID");

            migrationBuilder.CreateIndex(
                name: "IX_UserCorporateProfile_fk_UserID",
                table: "UserCorporateProfile",
                column: "fk_UserID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCandidateProfile");

            migrationBuilder.DropTable(
                name: "UserCorporateProfile");

            migrationBuilder.DropTable(
                name: "Corporate");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2022, 9, 13, 16, 18, 5, 608, DateTimeKind.Utc).AddTicks(3032), "Op5iRyhQ8QrSiRlHgHIYOf41wWcfYokbGOWvhPrLEqKm1XKb" });

            migrationBuilder.UpdateData(
                table: "UserRole",
                keyColumn: "ID",
                keyValue: 1,
                column: "CanViewUsers",
                value: false);
        }
    }
}
