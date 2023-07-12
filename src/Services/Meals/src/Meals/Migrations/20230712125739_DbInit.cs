using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meals.Migrations
{
    /// <inheritdoc />
    public partial class DbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ingredients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users_meals",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_meals", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "meals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    meal_name = table.Column<string>(type: "text", nullable: false),
                    meal_review = table.Column<string>(type: "text", nullable: true),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    instructions = table.Column<string>(type: "text", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meals", x => x.id);
                    table.ForeignKey(
                        name: "FK_meals_users_meals_owner_id",
                        column: x => x.owner_id,
                        principalTable: "users_meals",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "liked_meals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    meal_id = table.Column<Guid>(type: "uuid", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_liked_meals", x => x.id);
                    table.ForeignKey(
                        name: "FK_liked_meals_meals_meal_id",
                        column: x => x.meal_id,
                        principalTable: "meals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_liked_meals_users_meals_owner_id",
                        column: x => x.owner_id,
                        principalTable: "users_meals",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "meal_category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    meal_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meal_category", x => x.id);
                    table.ForeignKey(
                        name: "FK_meal_category_category_category_id",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_meal_category_meals_meal_id",
                        column: x => x.meal_id,
                        principalTable: "meals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "meal_ingredients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ingredient_id = table.Column<Guid>(type: "uuid", nullable: false),
                    meal_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meal_ingredients", x => x.id);
                    table.ForeignKey(
                        name: "FK_meal_ingredients_ingredients_ingredient_id",
                        column: x => x.ingredient_id,
                        principalTable: "ingredients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_meal_ingredients_meals_meal_id",
                        column: x => x.meal_id,
                        principalTable: "meals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_liked_meals_meal_id",
                table: "liked_meals",
                column: "meal_id");

            migrationBuilder.CreateIndex(
                name: "IX_liked_meals_owner_id",
                table: "liked_meals",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_category_category_id",
                table: "meal_category",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_category_meal_id",
                table: "meal_category",
                column: "meal_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_ingredients_ingredient_id",
                table: "meal_ingredients",
                column: "ingredient_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_ingredients_meal_id",
                table: "meal_ingredients",
                column: "meal_id");

            migrationBuilder.CreateIndex(
                name: "IX_meals_owner_id",
                table: "meals",
                column: "owner_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "liked_meals");

            migrationBuilder.DropTable(
                name: "meal_category");

            migrationBuilder.DropTable(
                name: "meal_ingredients");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "ingredients");

            migrationBuilder.DropTable(
                name: "meals");

            migrationBuilder.DropTable(
                name: "users_meals");
        }
    }
}
