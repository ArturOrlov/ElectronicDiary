using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronicDiary.Migrations
{
    public partial class AddRefreshTokenInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timetable_Class_SchoolClassId",
                schema: "School",
                table: "Timetable");

            migrationBuilder.DropColumn(
                name: "ClassId",
                schema: "School",
                table: "Timetable");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                schema: "Identity",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                schema: "Identity",
                table: "User",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "SchoolClassId",
                schema: "School",
                table: "Timetable",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Timetable_Class_SchoolClassId",
                schema: "School",
                table: "Timetable",
                column: "SchoolClassId",
                principalSchema: "School",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timetable_Class_SchoolClassId",
                schema: "School",
                table: "Timetable");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                schema: "Identity",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "SchoolClassId",
                schema: "School",
                table: "Timetable",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                schema: "School",
                table: "Timetable",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Timetable_Class_SchoolClassId",
                schema: "School",
                table: "Timetable",
                column: "SchoolClassId",
                principalSchema: "School",
                principalTable: "Class",
                principalColumn: "Id");
        }
    }
}
