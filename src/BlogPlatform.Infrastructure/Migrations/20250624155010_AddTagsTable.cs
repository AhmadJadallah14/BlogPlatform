using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTagsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Posts_PostId",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Tag_TagId",
                table: "PostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "PostTag",
                newName: "PostTags");

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_TagId",
                table: "PostTags",
                newName: "IX_PostTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_PostId",
                table: "PostTags",
                newName: "IX_PostTags_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTags",
                table: "PostTags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Tags_TagId",
                table: "PostTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Tags_TagId",
                table: "PostTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTags",
                table: "PostTags");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "PostTags",
                newName: "PostTag");

            migrationBuilder.RenameIndex(
                name: "IX_PostTags_TagId",
                table: "PostTag",
                newName: "IX_PostTag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_PostTags_PostId",
                table: "PostTag",
                newName: "IX_PostTag_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Posts_PostId",
                table: "PostTag",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tag_TagId",
                table: "PostTag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
