using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViteLoq.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AppUserPropAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNutritionEntries_AspNetUsers_AppUserId",
                table: "UserNutritionEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutTemplates",
                table: "WorkoutTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserNutritionEntries",
                table: "UserNutritionEntries");

            migrationBuilder.RenameTable(
                name: "WorkoutTemplates",
                newName: "WorkoutTemplate");

            migrationBuilder.RenameTable(
                name: "UserNutritionEntries",
                newName: "UserFoodEntries");

            migrationBuilder.RenameIndex(
                name: "IX_UserNutritionEntries_AppUserId",
                table: "UserFoodEntries",
                newName: "IX_UserFoodEntries_AppUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NutritionItems",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "NutritionItems",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "AspNetUsers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutTemplate",
                table: "WorkoutTemplate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFoodEntries",
                table: "UserFoodEntries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFoodEntries_AspNetUsers_AppUserId",
                table: "UserFoodEntries",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFoodEntries_AspNetUsers_AppUserId",
                table: "UserFoodEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutTemplate",
                table: "WorkoutTemplate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFoodEntries",
                table: "UserFoodEntries");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "WorkoutTemplate",
                newName: "WorkoutTemplates");

            migrationBuilder.RenameTable(
                name: "UserFoodEntries",
                newName: "UserNutritionEntries");

            migrationBuilder.RenameIndex(
                name: "IX_UserFoodEntries_AppUserId",
                table: "UserNutritionEntries",
                newName: "IX_UserNutritionEntries_AppUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NutritionItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "NutritionItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutTemplates",
                table: "WorkoutTemplates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserNutritionEntries",
                table: "UserNutritionEntries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNutritionEntries_AspNetUsers_AppUserId",
                table: "UserNutritionEntries",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
