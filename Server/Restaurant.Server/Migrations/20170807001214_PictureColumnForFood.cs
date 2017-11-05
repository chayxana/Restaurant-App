using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Server.Api.Migrations
{
	[ExcludeFromCodeCoverage]
	public partial class PictureColumnForFood : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				"Picture",
				"Foods",
				nullable: false,
				defaultValue: "");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				"Picture",
				"Foods");
		}
	}
}