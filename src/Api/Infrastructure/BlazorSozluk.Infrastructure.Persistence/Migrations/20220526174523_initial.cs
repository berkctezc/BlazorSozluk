using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorSozluk.Infrastructure.Persistence.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "user",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OldEmailAddress = table.Column<string>(type: "text", nullable: false),
                    NewEmailAddress = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "entry",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entry_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entrycomment",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    EntryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entrycomment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entrycomment_entry_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "public",
                        principalTable: "entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entrycomment_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entryfavorite",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entryfavorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entryfavorite_entry_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "public",
                        principalTable: "entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entryfavorite_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entryvote",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntryId = table.Column<Guid>(type: "uuid", nullable: false),
                    VoteType = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entryvote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entryvote_entry_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "public",
                        principalTable: "entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entrycommentfavorite",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntryCommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entrycommentfavorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entrycommentfavorite_entrycomment_EntryCommentId",
                        column: x => x.EntryCommentId,
                        principalSchema: "public",
                        principalTable: "entrycomment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entrycommentfavorite_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entrycommentvote",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntryCommentVoteId = table.Column<Guid>(type: "uuid", nullable: false),
                    VoteType = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    EntryCommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entrycommentvote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entrycommentvote_entrycomment_EntryCommentId",
                        column: x => x.EntryCommentId,
                        principalSchema: "public",
                        principalTable: "entrycomment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_entry_CreatedById",
                schema: "public",
                table: "entry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_entrycomment_CreatedById",
                schema: "public",
                table: "entrycomment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_entrycomment_EntryId",
                schema: "public",
                table: "entrycomment",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_entrycommentfavorite_CreatedById",
                schema: "public",
                table: "entrycommentfavorite",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_entrycommentfavorite_EntryCommentId",
                schema: "public",
                table: "entrycommentfavorite",
                column: "EntryCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_entrycommentvote_EntryCommentId",
                schema: "public",
                table: "entrycommentvote",
                column: "EntryCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_entryfavorite_CreatedById",
                schema: "public",
                table: "entryfavorite",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_entryfavorite_EntryId",
                schema: "public",
                table: "entryfavorite",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_entryvote_EntryId",
                schema: "public",
                table: "entryvote",
                column: "EntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entrycommentfavorite",
                schema: "public");

            migrationBuilder.DropTable(
                name: "entrycommentvote",
                schema: "public");

            migrationBuilder.DropTable(
                name: "entryfavorite",
                schema: "public");

            migrationBuilder.DropTable(
                name: "entryvote",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user",
                schema: "public");

            migrationBuilder.DropTable(
                name: "entrycomment",
                schema: "public");

            migrationBuilder.DropTable(
                name: "entry",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
