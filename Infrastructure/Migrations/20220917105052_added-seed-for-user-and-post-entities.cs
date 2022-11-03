using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addedseedforuserandpostentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostCommentSetting",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCommentSetting", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PostReactionType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IconKey = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReactionType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PostType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PostViewSetting",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostViewSetting", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    fk_CommentSettingID = table.Column<int>(type: "int", nullable: false),
                    fk_PostViewSettingID = table.Column<int>(type: "int", nullable: false),
                    fk_PostTypeID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Post_PostCommentSetting_fk_CommentSettingID",
                        column: x => x.fk_CommentSettingID,
                        principalTable: "PostCommentSetting",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Post_PostType_fk_PostTypeID",
                        column: x => x.fk_PostTypeID,
                        principalTable: "PostType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Post_PostViewSetting_fk_PostViewSettingID",
                        column: x => x.fk_PostViewSettingID,
                        principalTable: "PostViewSetting",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostComment",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    fk_PostID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostComment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PostComment_Post_fk_PostID",
                        column: x => x.fk_PostID,
                        principalTable: "Post",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostFile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileKey = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    MimeType = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    fk_PostID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostFile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PostFile_Post_fk_PostID",
                        column: x => x.fk_PostID,
                        principalTable: "Post",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostReaction",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_PostReactionTypeID = table.Column<int>(type: "int", nullable: false),
                    fk_PostID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReaction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PostReaction_Post_fk_PostID",
                        column: x => x.fk_PostID,
                        principalTable: "Post",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostReaction_PostReactionType_fk_PostReactionTypeID",
                        column: x => x.fk_PostReactionTypeID,
                        principalTable: "PostReactionType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostShare",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_PostID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostShare", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PostShare_Post_fk_PostID",
                        column: x => x.fk_PostID,
                        principalTable: "Post",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    fk_PostID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PostTag_Post_fk_PostID",
                        column: x => x.fk_PostID,
                        principalTable: "Post",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PostCommentSetting",
                columns: new[] { "ID", "CreatedAt", "CreatedBy", "Description", "IsActive", "IsDeleted", "Label", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { 1, null, null, "No one is allowed to comment", true, false, "Turned Off", null, null },
                    { 2, null, null, "Only I can comment on this post", true, false, "Only Me", null, null }
                });

            migrationBuilder.InsertData(
                table: "PostReactionType",
                columns: new[] { "ID", "CreatedAt", "CreatedBy", "IconKey", "IsActive", "IsDeleted", "Label", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { 1, null, null, null, true, false, "Like", null, null },
                    { 2, null, null, null, true, false, "Curious", null, null },
                    { 3, null, null, null, true, false, "Helped", null, null },
                    { 4, null, null, null, true, false, "Support", null, null }
                });

            migrationBuilder.InsertData(
                table: "PostType",
                columns: new[] { "ID", "CreatedAt", "CreatedBy", "Description", "IsActive", "IsDeleted", "Label", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { 1, null, null, null, true, false, "Ordinary", null, null },
                    { 2, null, null, null, true, false, "Job", null, null },
                    { 3, null, null, null, true, false, "Poll", null, null },
                    { 4, null, null, null, true, false, "Celebration", null, null }
                });

            migrationBuilder.InsertData(
                table: "PostViewSetting",
                columns: new[] { "ID", "CreatedAt", "CreatedBy", "Description", "IsActive", "IsDeleted", "Label", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { 1, null, null, "Anyone can view this post", true, false, "Anyone", null, null },
                    { 2, null, null, "No one can view this post", true, false, "Only Me", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_fk_CommentSettingID",
                table: "Post",
                column: "fk_CommentSettingID");

            migrationBuilder.CreateIndex(
                name: "IX_Post_fk_PostTypeID",
                table: "Post",
                column: "fk_PostTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Post_fk_PostViewSettingID",
                table: "Post",
                column: "fk_PostViewSettingID");

            migrationBuilder.CreateIndex(
                name: "IX_PostComment_fk_PostID",
                table: "PostComment",
                column: "fk_PostID");

            migrationBuilder.CreateIndex(
                name: "IX_PostFile_fk_PostID",
                table: "PostFile",
                column: "fk_PostID");

            migrationBuilder.CreateIndex(
                name: "IX_PostReaction_fk_PostID",
                table: "PostReaction",
                column: "fk_PostID");

            migrationBuilder.CreateIndex(
                name: "IX_PostReaction_fk_PostReactionTypeID",
                table: "PostReaction",
                column: "fk_PostReactionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PostShare_fk_PostID",
                table: "PostShare",
                column: "fk_PostID");

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_fk_PostID",
                table: "PostTag",
                column: "fk_PostID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostComment");

            migrationBuilder.DropTable(
                name: "PostFile");

            migrationBuilder.DropTable(
                name: "PostReaction");

            migrationBuilder.DropTable(
                name: "PostShare");

            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.DropTable(
                name: "PostReactionType");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "PostCommentSetting");

            migrationBuilder.DropTable(
                name: "PostType");

            migrationBuilder.DropTable(
                name: "PostViewSetting");
        }
    }
}
