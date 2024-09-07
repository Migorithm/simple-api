using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class addContentToComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CretedOn",
                table: "Comment",
                newName: "CreatedOn");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Comment",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Comment",
                newName: "CretedOn");
        }
    }
}
