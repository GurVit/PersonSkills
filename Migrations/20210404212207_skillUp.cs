using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonSkill.Migrations
{
    public partial class skillUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Persons_personid",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_personid",
                table: "Skills");

            migrationBuilder.AlterColumn<string>(
                name: "skillName",
                table: "Skills",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "personid",
                table: "Skills",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                columns: new[] { "personid", "skillName" });

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Persons_personid",
                table: "Skills",
                column: "personid",
                principalTable: "Persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Persons_personid",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.AlterColumn<string>(
                name: "skillName",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<long>(
                name: "personid",
                table: "Skills",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "id",
                table: "Skills",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_personid",
                table: "Skills",
                column: "personid");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Persons_personid",
                table: "Skills",
                column: "personid",
                principalTable: "Persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
