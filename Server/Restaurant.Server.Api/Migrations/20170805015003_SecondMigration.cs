using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Server.Api.Migrations
{
	[ExcludeFromCodeCoverage]
	public partial class SecondMigration : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				"ShortName",
				"Categories");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				"ShortName",
				"Categories",
				nullable: true);
		}
	}
}