using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class candidatemoduleaddedentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CandidateJobExperience",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CorporateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPresentExperience = table.Column<bool>(type: "bit", nullable: false),
                    fk_CandidateProfileID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateJobExperience", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CandidateJobExperience_UserCandidateProfile_fk_CandidateProfileID",
                        column: x => x.fk_CandidateProfileID,
                        principalTable: "UserCandidateProfile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationSubject",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationSubject", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CandidateEducationExperience",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelOfEdujcation = table.Column<int>(type: "int", nullable: false),
                    fk_SubjectID = table.Column<int>(type: "int", nullable: false),
                    InstitueName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPresentExperience = table.Column<bool>(type: "bit", nullable: false),
                    fk_CandidateProfileID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateEducationExperience", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CandidateEducationExperience_EducationSubject_fk_SubjectID",
                        column: x => x.fk_SubjectID,
                        principalTable: "EducationSubject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateEducationExperience_UserCandidateProfile_fk_CandidateProfileID",
                        column: x => x.fk_CandidateProfileID,
                        principalTable: "UserCandidateProfile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EducationSubject",
                columns: new[] { "ID", "CreatedAt", "CreatedBy", "IsActive", "IsDeleted", "ModifiedAt", "ModifiedBy", "Subject" },
                values: new object[,]
                {
                    { 1, null, null, true, false, null, null, "Business" },
                    { 2, null, null, true, false, null, null, "Computer Science" },
                    { 3, null, null, true, false, null, null, "Medical" },
                    { 4, null, null, true, false, null, null, "Engineering" }
                });

            migrationBuilder.InsertData(
                table: "PostCommentSetting",
                columns: new[] { "ID", "CreatedAt", "CreatedBy", "Description", "IsActive", "IsDeleted", "Label", "ModifiedAt", "ModifiedBy" },
                values: new object[] { 3, null, null, "Everyone can comment on this post", true, false, "Default", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducationExperience_fk_CandidateProfileID",
                table: "CandidateEducationExperience",
                column: "fk_CandidateProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducationExperience_fk_SubjectID",
                table: "CandidateEducationExperience",
                column: "fk_SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateJobExperience_fk_CandidateProfileID",
                table: "CandidateJobExperience",
                column: "fk_CandidateProfileID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateEducationExperience");

            migrationBuilder.DropTable(
                name: "CandidateJobExperience");

            migrationBuilder.DropTable(
                name: "EducationSubject");

            migrationBuilder.DeleteData(
                table: "PostCommentSetting",
                keyColumn: "ID",
                keyValue: 3);
            
        }
    }
}
