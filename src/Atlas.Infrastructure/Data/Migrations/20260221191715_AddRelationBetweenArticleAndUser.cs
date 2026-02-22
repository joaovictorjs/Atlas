using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atlas.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenArticleAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "creator_id",
                table: "articles",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<int>(
                name: "publisher_id",
                table: "articles",
                type: "integer",
                nullable: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_articles_creator_id",
                table: "articles",
                column: "creator_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_articles_publisher_id",
                table: "articles",
                column: "publisher_id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_articles_users_creator_id",
                table: "articles",
                column: "creator_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict
            );

            migrationBuilder.AddForeignKey(
                name: "FK_articles_users_publisher_id",
                table: "articles",
                column: "publisher_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articles_users_creator_id",
                table: "articles"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_articles_users_publisher_id",
                table: "articles"
            );

            migrationBuilder.DropIndex(name: "IX_articles_creator_id", table: "articles");

            migrationBuilder.DropIndex(name: "IX_articles_publisher_id", table: "articles");

            migrationBuilder.DropColumn(name: "creator_id", table: "articles");

            migrationBuilder.DropColumn(name: "publisher_id", table: "articles");
        }
    }
}
