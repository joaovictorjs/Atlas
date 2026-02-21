using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atlas.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenUserAndCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "creator_id",
                table: "categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_categories_creator_id",
                table: "categories",
                column: "creator_id");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_users_creator_id",
                table: "categories",
                column: "creator_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_users_creator_id",
                table: "categories");

            migrationBuilder.DropIndex(
                name: "IX_categories_creator_id",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "creator_id",
                table: "categories");
        }
    }
}
