using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STProject.Migrations
{
    public partial class CommunityUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communities_AspNetUsers_OwnerId",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Communities");

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_AspNetUsers_OwnerId",
                table: "Communities",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communities_AspNetUsers_OwnerId",
                table: "Communities");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Communities",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CommunityServiceModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityServiceModel", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_AspNetUsers_OwnerId",
                table: "Communities",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
