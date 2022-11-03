using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class updatedcandidateentites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "UserCandidateProfile",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacecbookUserName",
                table: "UserCandidateProfile",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "UserCandidateProfile",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedInUserName",
                table: "UserCandidateProfile",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                table: "UserCandidateProfile");

            migrationBuilder.DropColumn(
                name: "FacecbookUserName",
                table: "UserCandidateProfile");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "UserCandidateProfile");

            migrationBuilder.DropColumn(
                name: "LinkedInUserName",
                table: "UserCandidateProfile");
        }
    }
}
