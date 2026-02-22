using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atlas.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenUserAndTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "creator_id",
                table: "tags",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.CreateIndex(
                name: "IX_tags_creator_id",
                table: "tags",
                column: "creator_id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_tags_users_creator_id",
                table: "tags",
                column: "creator_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_tags_users_creator_id", table: "tags");

            migrationBuilder.DropIndex(name: "IX_tags_creator_id", table: "tags");

            migrationBuilder.DropColumn(name: "creator_id", table: "tags");
        }
    }
}
