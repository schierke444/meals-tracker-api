using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meals.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMeals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealIngredients_Ingredients_IngredientId",
                table: "MealIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_MealIngredients_Meals_MealId",
                table: "MealIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meals",
                table: "Meals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealIngredients",
                table: "MealIngredients");

            migrationBuilder.RenameTable(
                name: "Meals",
                newName: "meals");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "ingredients");

            migrationBuilder.RenameTable(
                name: "MealIngredients",
                newName: "meal_ingredients");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "meals",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "meals",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "meals",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "meals",
                newName: "owner_id");

            migrationBuilder.RenameColumn(
                name: "MealReview",
                table: "meals",
                newName: "meal_review");

            migrationBuilder.RenameColumn(
                name: "MealName",
                table: "meals",
                newName: "meal_name");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "meals",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "meals",
                newName: "category_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ingredients",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ingredients",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "ingredients",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ingredients",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "meal_ingredients",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "meal_ingredients",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "MealId",
                table: "meal_ingredients",
                newName: "meal_id");

            migrationBuilder.RenameColumn(
                name: "IngredientId",
                table: "meal_ingredients",
                newName: "ingredient_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "meal_ingredients",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_MealIngredients_MealId",
                table: "meal_ingredients",
                newName: "IX_meal_ingredients_meal_id");

            migrationBuilder.RenameIndex(
                name: "IX_MealIngredients_IngredientId",
                table: "meal_ingredients",
                newName: "IX_meal_ingredients_ingredient_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_meals",
                table: "meals",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ingredients",
                table: "ingredients",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_meal_ingredients",
                table: "meal_ingredients",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_meal_ingredients_ingredients_ingredient_id",
                table: "meal_ingredients",
                column: "ingredient_id",
                principalTable: "ingredients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_meal_ingredients_meals_meal_id",
                table: "meal_ingredients",
                column: "meal_id",
                principalTable: "meals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_meal_ingredients_ingredients_ingredient_id",
                table: "meal_ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_meal_ingredients_meals_meal_id",
                table: "meal_ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_meals",
                table: "meals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ingredients",
                table: "ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_meal_ingredients",
                table: "meal_ingredients");

            migrationBuilder.RenameTable(
                name: "meals",
                newName: "Meals");

            migrationBuilder.RenameTable(
                name: "ingredients",
                newName: "Ingredients");

            migrationBuilder.RenameTable(
                name: "meal_ingredients",
                newName: "MealIngredients");

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "Meals",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Meals",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Meals",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "owner_id",
                table: "Meals",
                newName: "OwnerId");

            migrationBuilder.RenameColumn(
                name: "meal_review",
                table: "Meals",
                newName: "MealReview");

            migrationBuilder.RenameColumn(
                name: "meal_name",
                table: "Meals",
                newName: "MealName");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Meals",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "Meals",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Ingredients",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Ingredients",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Ingredients",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Ingredients",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "MealIngredients",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "MealIngredients",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "meal_id",
                table: "MealIngredients",
                newName: "MealId");

            migrationBuilder.RenameColumn(
                name: "ingredient_id",
                table: "MealIngredients",
                newName: "IngredientId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "MealIngredients",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_meal_ingredients_meal_id",
                table: "MealIngredients",
                newName: "IX_MealIngredients_MealId");

            migrationBuilder.RenameIndex(
                name: "IX_meal_ingredients_ingredient_id",
                table: "MealIngredients",
                newName: "IX_MealIngredients_IngredientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meals",
                table: "Meals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealIngredients",
                table: "MealIngredients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MealIngredients_Ingredients_IngredientId",
                table: "MealIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealIngredients_Meals_MealId",
                table: "MealIngredients",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
