using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addedjobentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CorporateJob",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_CorporateID = table.Column<int>(type: "int", nullable: false),
                    fk_PostID = table.Column<int>(type: "int", nullable: false),
                    fk_JobPostedByProfileID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporateJob", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CorporateJob_Corporate_fk_CorporateID",
                        column: x => x.fk_CorporateID,
                        principalTable: "Corporate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CorporateJob_Post_fk_PostID",
                        column: x => x.fk_PostID,
                        principalTable: "Post",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CorporateJob_UserCorporateProfile_fk_JobPostedByProfileID",
                        column: x => x.fk_JobPostedByProfileID,
                        principalTable: "UserCorporateProfile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JobApplicant",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_CorporateJobID = table.Column<int>(type: "int", nullable: false),
                    fk_CandidateResumeID = table.Column<int>(type: "int", nullable: false),
                    fk_CandidateProfileID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplicant", x => x.ID);
                    table.ForeignKey(
                        name: "FK_JobApplicant_CandidateResumeUploadDetail_fk_CandidateResumeID",
                        column: x => x.fk_CandidateResumeID,
                        principalTable: "CandidateResumeUploadDetail",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobApplicant_CorporateJob_fk_CorporateJobID",
                        column: x => x.fk_CorporateJobID,
                        principalTable: "CorporateJob",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobApplicant_UserCandidateProfile_fk_CandidateProfileID",
                        column: x => x.fk_CandidateProfileID,
                        principalTable: "UserCandidateProfile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorporateJob_fk_CorporateID",
                table: "CorporateJob",
                column: "fk_CorporateID");

            migrationBuilder.CreateIndex(
                name: "IX_CorporateJob_fk_JobPostedByProfileID",
                table: "CorporateJob",
                column: "fk_JobPostedByProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_CorporateJob_fk_PostID",
                table: "CorporateJob",
                column: "fk_PostID");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicant_fk_CandidateProfileID",
                table: "JobApplicant",
                column: "fk_CandidateProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicant_fk_CandidateResumeID",
                table: "JobApplicant",
                column: "fk_CandidateResumeID");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicant_fk_CorporateJobID",
                table: "JobApplicant",
                column: "fk_CorporateJobID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobApplicant");

            migrationBuilder.DropTable(
                name: "CorporateJob");
        }
    }
}
