using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonSkill.Migrations
{
    public partial class PascalCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Persons_personid",
                table: "Skills");

            migrationBuilder.RenameColumn(
                name: "level",
                table: "Skills",
                newName: "Level");

            migrationBuilder.RenameColumn(
                name: "skillName",
                table: "Skills",
                newName: "SkillName");

            migrationBuilder.RenameColumn(
                name: "personid",
                table: "Skills",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Persons",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "displayName",
                table: "Persons",
                newName: "DisplayName");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Persons",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Persons_PersonId",
                table: "Skills",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Persons_PersonId",
                table: "Skills");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "Skills",
                newName: "level");

            migrationBuilder.RenameColumn(
                name: "SkillName",
                table: "Skills",
                newName: "skillName");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Skills",
                newName: "personid");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Persons",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "Persons",
                newName: "displayName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Persons",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Persons_personid",
                table: "Skills",
                column: "personid",
                principalTable: "Persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
