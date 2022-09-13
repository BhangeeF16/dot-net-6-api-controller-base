using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class inituserandrolewithseedofadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiCallLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndPoint = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    RequestUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseStatusCode = table.Column<int>(type: "int", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSuccessfull = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsException = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiCallLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Label = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MiddlewareLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestURL = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RequestByURL = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    RequestBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseStatusCode = table.Column<int>(type: "int", nullable: false),
                    RequestAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponseAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiddlewareLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CanAddUser = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CanViewUsers = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CanEditUser = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CanDeleteUser = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Ethnicity = table.Column<int>(type: "int", nullable: true),
                    CommunicationPreference = table.Column<int>(type: "int", nullable: true),
                    ImageKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsOnBoarded = table.Column<bool>(type: "bit", nullable: true),
                    IsPasswordChanged = table.Column<bool>(type: "bit", nullable: false),
                    fk_RoleID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_UserRole_fk_RoleID",
                        column: x => x.fk_RoleID,
                        principalTable: "UserRole",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "ID", "CanAddUser", "CanDeleteUser", "CanEditUser", "CanViewUsers", "CreatedAt", "CreatedBy", "IsActive", "IsDeleted", "ModifiedAt", "ModifiedBy", "RoleName" },
                values: new object[] { 1, true, true, true, false, null, null, true, false, null, null, "Application Admin" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "Address", "CommunicationPreference", "CreatedAt", "CreatedBy", "DOB", "Ethnicity", "FirstName", "Gender", "ImageKey", "IsActive", "IsDeleted", "IsOnBoarded", "IsPasswordChanged", "LastName", "ModifiedAt", "ModifiedBy", "Password", "PhoneNumber", "UserName", "fk_RoleID" },
                values: new object[] { 1, "", 3, null, null, new DateTime(2022, 9, 12, 17, 32, 25, 881, DateTimeKind.Utc).AddTicks(5335), 2, "Application", 1, "", true, false, true, true, "Admin", null, null, "GkmYMta7A5B/egqMSaWB77l+i5Y/ffH17NZKgT+gV9AcgirfiA==", "", "admin@codered.com", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_User_fk_RoleID",
                table: "User",
                column: "fk_RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiCallLog");

            migrationBuilder.DropTable(
                name: "AppSetting");

            migrationBuilder.DropTable(
                name: "MiddlewareLog");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserRole");
        }
    }
}
