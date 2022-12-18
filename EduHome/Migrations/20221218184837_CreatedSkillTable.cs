using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHome.Migrations
{
    public partial class CreatedSkillTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSkill_Skill_SkillId",
                table: "TeacherSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSkill_Teachers_TeacherId",
                table: "TeacherSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherSkill",
                table: "TeacherSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skill",
                table: "Skill");

            migrationBuilder.RenameTable(
                name: "TeacherSkill",
                newName: "TeacherSkills");

            migrationBuilder.RenameTable(
                name: "Skill",
                newName: "Skills");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSkill_TeacherId",
                table: "TeacherSkills",
                newName: "IX_TeacherSkills_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSkill_SkillId",
                table: "TeacherSkills",
                newName: "IX_TeacherSkills_SkillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherSkills",
                table: "TeacherSkills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSkills_Skills_SkillId",
                table: "TeacherSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSkills_Teachers_TeacherId",
                table: "TeacherSkills",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSkills_Skills_SkillId",
                table: "TeacherSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSkills_Teachers_TeacherId",
                table: "TeacherSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherSkills",
                table: "TeacherSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.RenameTable(
                name: "TeacherSkills",
                newName: "TeacherSkill");

            migrationBuilder.RenameTable(
                name: "Skills",
                newName: "Skill");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSkills_TeacherId",
                table: "TeacherSkill",
                newName: "IX_TeacherSkill_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSkills_SkillId",
                table: "TeacherSkill",
                newName: "IX_TeacherSkill_SkillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherSkill",
                table: "TeacherSkill",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skill",
                table: "Skill",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSkill_Skill_SkillId",
                table: "TeacherSkill",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSkill_Teachers_TeacherId",
                table: "TeacherSkill",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
