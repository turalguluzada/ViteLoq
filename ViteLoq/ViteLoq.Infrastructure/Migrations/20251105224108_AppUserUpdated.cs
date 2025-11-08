using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViteLoq.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AppUserUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserTotalHealth_UserTotalHealthId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserTotalMental_UserTotalMentalId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserTotalMuscle_UserTotalMuscleId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserTotalSkin_UserTotalSkinId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserTotalHealthId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserTotalMentalId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserTotalMuscleId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserTotalSkinId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserTotalHealthId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserTotalMentalId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserTotalMuscleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserTotalSkinId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserTotalHealthId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserTotalMentalId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserTotalMuscleId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserTotalSkinId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserTotalHealthId",
                table: "AspNetUsers",
                column: "UserTotalHealthId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserTotalMentalId",
                table: "AspNetUsers",
                column: "UserTotalMentalId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserTotalMuscleId",
                table: "AspNetUsers",
                column: "UserTotalMuscleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserTotalSkinId",
                table: "AspNetUsers",
                column: "UserTotalSkinId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserTotalHealth_UserTotalHealthId",
                table: "AspNetUsers",
                column: "UserTotalHealthId",
                principalTable: "UserTotalHealth",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserTotalMental_UserTotalMentalId",
                table: "AspNetUsers",
                column: "UserTotalMentalId",
                principalTable: "UserTotalMental",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserTotalMuscle_UserTotalMuscleId",
                table: "AspNetUsers",
                column: "UserTotalMuscleId",
                principalTable: "UserTotalMuscle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserTotalSkin_UserTotalSkinId",
                table: "AspNetUsers",
                column: "UserTotalSkinId",
                principalTable: "UserTotalSkin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
