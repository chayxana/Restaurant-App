using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Server.Api.Migrations
{
	[ExcludeFromCodeCoverage]
	public partial class FirsMigration : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				"AspNetRoles",
				table => new
				{
					Id = table.Column<string>(nullable: false),
					ConcurrencyStamp = table.Column<string>(nullable: true),
					Name = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
				},
				constraints: table => { table.PrimaryKey("PK_AspNetRoles", x => x.Id); });

			migrationBuilder.CreateTable(
				"AspNetUserTokens",
				table => new
				{
					UserId = table.Column<string>(nullable: false),
					LoginProvider = table.Column<string>(nullable: false),
					Name = table.Column<string>(nullable: false),
					Value = table.Column<string>(nullable: true)
				},
				constraints: table => { table.PrimaryKey("PK_AspNetUserTokens", x => new {x.UserId, x.LoginProvider, x.Name}); });

			migrationBuilder.CreateTable(
				"Categories",
				table => new
				{
					Id = table.Column<Guid>(nullable: false),
					Color = table.Column<string>(nullable: true),
					Name = table.Column<string>(nullable: false),
					ShortName = table.Column<string>(nullable: true)
				},
				constraints: table => { table.PrimaryKey("PK_Categories", x => x.Id); });

			migrationBuilder.CreateTable(
				"DailyEatings",
				table => new
				{
					Id = table.Column<Guid>(nullable: false),
					AdditionalAmount = table.Column<decimal>(nullable: false),
					Amount = table.Column<decimal>(nullable: false),
					DateTime = table.Column<DateTime>(nullable: false),
					Decsription = table.Column<string>(nullable: true),
					Reciept = table.Column<string>(nullable: true)
				},
				constraints: table => { table.PrimaryKey("PK_DailyEatings", x => x.Id); });

			migrationBuilder.CreateTable(
				"AspNetUsers",
				table => new
				{
					Id = table.Column<string>(nullable: false),
					AccessFailedCount = table.Column<int>(nullable: false),
					ConcurrencyStamp = table.Column<string>(nullable: true),
					Email = table.Column<string>(maxLength: 256, nullable: true),
					EmailConfirmed = table.Column<bool>(nullable: false),
					LockoutEnabled = table.Column<bool>(nullable: false),
					LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
					NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
					PasswordHash = table.Column<string>(nullable: true),
					PhoneNumber = table.Column<string>(nullable: true),
					PhoneNumberConfirmed = table.Column<bool>(nullable: false),
					SecurityStamp = table.Column<string>(nullable: true),
					TwoFactorEnabled = table.Column<bool>(nullable: false),
					UserName = table.Column<string>(maxLength: 256, nullable: true)
				},
				constraints: table => { table.PrimaryKey("PK_AspNetUsers", x => x.Id); });

			migrationBuilder.CreateTable(
				"AspNetRoleClaims",
				table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					ClaimType = table.Column<string>(nullable: true),
					ClaimValue = table.Column<string>(nullable: true),
					RoleId = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
					table.ForeignKey(
						"FK_AspNetRoleClaims_AspNetRoles_RoleId",
						x => x.RoleId,
						"AspNetRoles",
						"Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				"Foods",
				table => new
				{
					Id = table.Column<Guid>(nullable: false),
					CategoryId = table.Column<Guid>(nullable: false),
					Currency = table.Column<string>(nullable: true),
					Description = table.Column<string>(nullable: true),
					Name = table.Column<string>(nullable: false),
					Price = table.Column<decimal>(nullable: false),
					Recept = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Foods", x => x.Id);
					table.ForeignKey(
						"FK_Foods_Categories_CategoryId",
						x => x.CategoryId,
						"Categories",
						"Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				"AspNetUserClaims",
				table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					ClaimType = table.Column<string>(nullable: true),
					ClaimValue = table.Column<string>(nullable: true),
					UserId = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
					table.ForeignKey(
						"FK_AspNetUserClaims_AspNetUsers_UserId",
						x => x.UserId,
						"AspNetUsers",
						"Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				"AspNetUserLogins",
				table => new
				{
					LoginProvider = table.Column<string>(nullable: false),
					ProviderKey = table.Column<string>(nullable: false),
					ProviderDisplayName = table.Column<string>(nullable: true),
					UserId = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserLogins", x => new {x.LoginProvider, x.ProviderKey});
					table.ForeignKey(
						"FK_AspNetUserLogins_AspNetUsers_UserId",
						x => x.UserId,
						"AspNetUsers",
						"Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				"AspNetUserRoles",
				table => new
				{
					UserId = table.Column<string>(nullable: false),
					RoleId = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserRoles", x => new {x.UserId, x.RoleId});
					table.ForeignKey(
						"FK_AspNetUserRoles_AspNetRoles_RoleId",
						x => x.RoleId,
						"AspNetRoles",
						"Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						"FK_AspNetUserRoles_AspNetUsers_UserId",
						x => x.UserId,
						"AspNetUsers",
						"Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				"Orders",
				table => new
				{
					Id = table.Column<Guid>(nullable: false),
					DateTime = table.Column<DateTime>(nullable: false),
					EatingId = table.Column<Guid>(nullable: false),
					UserId = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Orders", x => x.Id);
					table.ForeignKey(
						"FK_Orders_DailyEatings_EatingId",
						x => x.EatingId,
						"DailyEatings",
						"Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						"FK_Orders_AspNetUsers_UserId",
						x => x.UserId,
						"AspNetUsers",
						"Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				"UserProfile",
				table => new
				{
					Id = table.Column<Guid>(nullable: false),
					Picture = table.Column<string>(nullable: true),
					UserId = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserProfile", x => x.Id);
					table.ForeignKey(
						"FK_UserProfile_AspNetUsers_UserId",
						x => x.UserId,
						"AspNetUsers",
						"Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				"FavoriteFoods",
				table => new
				{
					FoodId = table.Column<Guid>(nullable: false),
					UserId = table.Column<string>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_FavoriteFoods", x => new {x.FoodId, x.UserId});
					table.ForeignKey(
						"FK_FavoriteFoods_Foods_FoodId",
						x => x.FoodId,
						"Foods",
						"Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				"OrderItems",
				table => new
				{
					FoodId = table.Column<Guid>(nullable: false),
					OderId = table.Column<Guid>(nullable: false),
					Quantity = table.Column<decimal>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_OrderItems", x => new {x.FoodId, x.OderId});
					table.ForeignKey(
						"FK_OrderItems_Foods_FoodId",
						x => x.FoodId,
						"Foods",
						"Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						"FK_OrderItems_Orders_OderId",
						x => x.OderId,
						"Orders",
						"Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				"RoleNameIndex",
				"AspNetRoles",
				"NormalizedName",
				unique: true);

			migrationBuilder.CreateIndex(
				"IX_AspNetRoleClaims_RoleId",
				"AspNetRoleClaims",
				"RoleId");

			migrationBuilder.CreateIndex(
				"IX_AspNetUserClaims_UserId",
				"AspNetUserClaims",
				"UserId");

			migrationBuilder.CreateIndex(
				"IX_AspNetUserLogins_UserId",
				"AspNetUserLogins",
				"UserId");

			migrationBuilder.CreateIndex(
				"IX_AspNetUserRoles_RoleId",
				"AspNetUserRoles",
				"RoleId");

			migrationBuilder.CreateIndex(
				"IX_Foods_CategoryId",
				"Foods",
				"CategoryId");

			migrationBuilder.CreateIndex(
				"IX_Orders_EatingId",
				"Orders",
				"EatingId");

			migrationBuilder.CreateIndex(
				"IX_Orders_UserId",
				"Orders",
				"UserId");

			migrationBuilder.CreateIndex(
				"IX_OrderItems_OderId",
				"OrderItems",
				"OderId");

			migrationBuilder.CreateIndex(
				"EmailIndex",
				"AspNetUsers",
				"NormalizedEmail");

			migrationBuilder.CreateIndex(
				"UserNameIndex",
				"AspNetUsers",
				"NormalizedUserName",
				unique: true);

			migrationBuilder.CreateIndex(
				"IX_UserProfile_UserId",
				"UserProfile",
				"UserId",
				unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				"AspNetRoleClaims");

			migrationBuilder.DropTable(
				"AspNetUserClaims");

			migrationBuilder.DropTable(
				"AspNetUserLogins");

			migrationBuilder.DropTable(
				"AspNetUserRoles");

			migrationBuilder.DropTable(
				"AspNetUserTokens");

			migrationBuilder.DropTable(
				"FavoriteFoods");

			migrationBuilder.DropTable(
				"OrderItems");

			migrationBuilder.DropTable(
				"UserProfile");

			migrationBuilder.DropTable(
				"AspNetRoles");

			migrationBuilder.DropTable(
				"Foods");

			migrationBuilder.DropTable(
				"Orders");

			migrationBuilder.DropTable(
				"Categories");

			migrationBuilder.DropTable(
				"DailyEatings");

			migrationBuilder.DropTable(
				"AspNetUsers");
		}
	}
}