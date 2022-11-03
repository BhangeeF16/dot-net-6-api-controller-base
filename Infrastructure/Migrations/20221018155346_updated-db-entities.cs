using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class updateddbentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "CorporateJob",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobType",
                table: "CorporateJob",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "CorporateJob",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkPlaceType",
                table: "CorporateJob",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Corporate",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Corporate",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfEmployees",
                table: "Corporate",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "CorporateJob");

            migrationBuilder.DropColumn(
                name: "JobType",
                table: "CorporateJob");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "CorporateJob");

            migrationBuilder.DropColumn(
                name: "WorkPlaceType",
                table: "CorporateJob");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Corporate");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Corporate");

            migrationBuilder.DropColumn(
                name: "NumberOfEmployees",
                table: "Corporate");
        }
    }
}
