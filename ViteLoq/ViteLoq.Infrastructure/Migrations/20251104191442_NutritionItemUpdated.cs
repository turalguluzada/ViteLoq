using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViteLoq.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NutritionItemUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allergens",
                table: "NutritionItems");

            migrationBuilder.DropColumn(
                name: "Categories",
                table: "NutritionItems");

            migrationBuilder.DropColumn(
                name: "IsPerishable",
                table: "NutritionItems");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "NutritionItems");

            migrationBuilder.DropColumn(
                name: "TypicalShelfLife",
                table: "NutritionItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Allergens",
                table: "NutritionItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Categories",
                table: "NutritionItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPerishable",
                table: "NutritionItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "NutritionItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TypicalShelfLife",
                table: "NutritionItems",
                type: "time",
                nullable: true);
        }
    }
}
