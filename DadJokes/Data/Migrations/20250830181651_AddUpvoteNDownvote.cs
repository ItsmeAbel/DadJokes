using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadJokes.Data.Migrations
{
    public partial class AddUpvoteNDownvote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Downvote",
                table: "Joke",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Upvote",
                table: "Joke",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Downvote",
                table: "Joke");

            migrationBuilder.DropColumn(
                name: "Upvote",
                table: "Joke");
        }
    }
}
