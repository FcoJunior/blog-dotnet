using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "post",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    thumbnail = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    WriterId = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post", x => x.id);
                    table.ForeignKey(
                        name: "FK_post_writer_WriterId",
                        column: x => x.WriterId,
                        principalSchema: "public",
                        principalTable: "writer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_post_WriterId",
                schema: "public",
                table: "post",
                column: "WriterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "post",
                schema: "public");
        }
    }
}
