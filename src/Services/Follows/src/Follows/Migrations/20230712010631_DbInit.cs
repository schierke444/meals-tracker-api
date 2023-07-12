using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Follows.Migrations
{
    /// <inheritdoc />
    public partial class DbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users_follows",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_follows", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "follows",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    follower_id = table.Column<Guid>(type: "uuid", nullable: false),
                    followee_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_follows", x => x.id);
                    table.ForeignKey(
                        name: "FK_follows_users_follows_followee_id",
                        column: x => x.followee_id,
                        principalTable: "users_follows",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_follows_users_follows_follower_id",
                        column: x => x.follower_id,
                        principalTable: "users_follows",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_follows_followee_id",
                table: "follows",
                column: "followee_id");

            migrationBuilder.CreateIndex(
                name: "IX_follows_follower_id",
                table: "follows",
                column: "follower_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "follows");

            migrationBuilder.DropTable(
                name: "users_follows");
        }
    }
}
