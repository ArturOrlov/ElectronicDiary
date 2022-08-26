using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ElectronicDiary.Migrations
{
    public partial class AddUserClassTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchoolClassId",
                schema: "School",
                table: "PerformanceRating",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserClass",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SchoolClassId = table.Column<int>(type: "integer", nullable: false),
                    IsClassroomTeacher = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClass_Class_SchoolClassId",
                        column: x => x.SchoolClassId,
                        principalSchema: "School",
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserClass_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceRating_SchoolClassId",
                schema: "School",
                table: "PerformanceRating",
                column: "SchoolClassId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClass_SchoolClassId",
                schema: "Identity",
                table: "UserClass",
                column: "SchoolClassId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClass_UserId",
                schema: "Identity",
                table: "UserClass",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceRating_Class_SchoolClassId",
                schema: "School",
                table: "PerformanceRating",
                column: "SchoolClassId",
                principalSchema: "School",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceRating_Class_SchoolClassId",
                schema: "School",
                table: "PerformanceRating");

            migrationBuilder.DropTable(
                name: "UserClass",
                schema: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_PerformanceRating_SchoolClassId",
                schema: "School",
                table: "PerformanceRating");

            migrationBuilder.DropColumn(
                name: "SchoolClassId",
                schema: "School",
                table: "PerformanceRating");
        }
    }
}
