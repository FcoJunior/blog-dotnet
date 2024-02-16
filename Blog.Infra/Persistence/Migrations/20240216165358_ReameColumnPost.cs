using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReameColumnPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_writer_WriterId",
                schema: "public",
                table: "post");

            migrationBuilder.RenameColumn(
                name: "WriterId",
                schema: "public",
                table: "post",
                newName: "writer_id");

            migrationBuilder.RenameIndex(
                name: "IX_post_WriterId",
                schema: "public",
                table: "post",
                newName: "IX_post_writer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_post_writer_writer_id",
                schema: "public",
                table: "post",
                column: "writer_id",
                principalSchema: "public",
                principalTable: "writer",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_writer_writer_id",
                schema: "public",
                table: "post");

            migrationBuilder.RenameColumn(
                name: "writer_id",
                schema: "public",
                table: "post",
                newName: "WriterId");

            migrationBuilder.RenameIndex(
                name: "IX_post_writer_id",
                schema: "public",
                table: "post",
                newName: "IX_post_WriterId");

            migrationBuilder.AddForeignKey(
                name: "FK_post_writer_WriterId",
                schema: "public",
                table: "post",
                column: "WriterId",
                principalSchema: "public",
                principalTable: "writer",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
