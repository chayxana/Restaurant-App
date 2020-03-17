using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Menu.API.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Food");

            migrationBuilder.CreateTable(
                name: "FoodPicture",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    OriginalFileName = table.Column<string>(nullable: true),
                    Length = table.Column<long>(nullable: false),
                    ContentType = table.Column<string>(nullable: true),
                    FoodId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodPicture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodPicture_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodPicture_FoodId",
                table: "FoodPicture",
                column: "FoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodPicture");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Food",
                nullable: false,
                defaultValue: "");
        }
    }
}
