﻿// <auto-generated />
using System;
using Meals.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Meals.Migrations
{
    [DbContext(typeof(MealsDbContext))]
    partial class MealsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Meals.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("category");
                });

            modelBuilder.Entity("Meals.Entities.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("ingredients");
                });

            modelBuilder.Entity("Meals.Entities.Meal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("instructions");

                    b.Property<string>("MealName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("meal_name");

                    b.Property<string>("MealReview")
                        .HasColumnType("text")
                        .HasColumnName("meal_review");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("owner_id");

                    b.Property<string>("OwnerName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("owner_name");

                    b.Property<int>("Rating")
                        .HasColumnType("integer")
                        .HasColumnName("rating");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("meals");
                });

            modelBuilder.Entity("Meals.Entities.MealCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("MealId")
                        .HasColumnType("uuid")
                        .HasColumnName("meal_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("MealId");

                    b.ToTable("meal_category");
                });

            modelBuilder.Entity("Meals.Entities.MealIngredients", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("IngredientId")
                        .HasColumnType("uuid")
                        .HasColumnName("ingredient_id");

                    b.Property<Guid>("MealId")
                        .HasColumnType("uuid")
                        .HasColumnName("meal_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("MealId");

                    b.ToTable("meal_ingredients");
                });

            modelBuilder.Entity("Meals.Entities.MealCategory", b =>
                {
                    b.HasOne("Meals.Entities.Category", "Category")
                        .WithMany("MealCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Meals.Entities.Meal", "Meal")
                        .WithMany("MealCategories")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Meal");
                });

            modelBuilder.Entity("Meals.Entities.MealIngredients", b =>
                {
                    b.HasOne("Meals.Entities.Ingredient", "Ingredients")
                        .WithMany("MealIngredient")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Meals.Entities.Meal", "Meals")
                        .WithMany("MealIngredient")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredients");

                    b.Navigation("Meals");
                });

            modelBuilder.Entity("Meals.Entities.Category", b =>
                {
                    b.Navigation("MealCategories");
                });

            modelBuilder.Entity("Meals.Entities.Ingredient", b =>
                {
                    b.Navigation("MealIngredient");
                });

            modelBuilder.Entity("Meals.Entities.Meal", b =>
                {
                    b.Navigation("MealCategories");

                    b.Navigation("MealIngredient");
                });
#pragma warning restore 612, 618
        }
    }
}
