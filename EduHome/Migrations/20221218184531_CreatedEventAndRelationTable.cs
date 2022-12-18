using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHome.Migrations
{
    public partial class CreatedEventAndRelationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_CourseCategories_CourseCategoryId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpeaker_Event_EventId",
                table: "EventSpeaker");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpeaker_Speaker_SpeakerId",
                table: "EventSpeaker");

            migrationBuilder.DropForeignKey(
                name: "FK_EventTag_Event_EventId",
                table: "EventTag");

            migrationBuilder.DropForeignKey(
                name: "FK_EventTag_Tags_TagId",
                table: "EventTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Speaker",
                table: "Speaker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventTag",
                table: "EventTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventSpeaker",
                table: "EventSpeaker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.RenameTable(
                name: "Speaker",
                newName: "Speakers");

            migrationBuilder.RenameTable(
                name: "EventTag",
                newName: "EventTags");

            migrationBuilder.RenameTable(
                name: "EventSpeaker",
                newName: "EventSpeakers");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Events");

            migrationBuilder.RenameIndex(
                name: "IX_EventTag_TagId",
                table: "EventTags",
                newName: "IX_EventTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_EventTag_EventId",
                table: "EventTags",
                newName: "IX_EventTags_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_EventSpeaker_SpeakerId",
                table: "EventSpeakers",
                newName: "IX_EventSpeakers_SpeakerId");

            migrationBuilder.RenameIndex(
                name: "IX_EventSpeaker_EventId",
                table: "EventSpeakers",
                newName: "IX_EventSpeakers_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_CourseCategoryId",
                table: "Events",
                newName: "IX_Events_CourseCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Speakers",
                table: "Speakers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventTags",
                table: "EventTags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventSpeakers",
                table: "EventSpeakers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_CourseCategories_CourseCategoryId",
                table: "Events",
                column: "CourseCategoryId",
                principalTable: "CourseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpeakers_Events_EventId",
                table: "EventSpeakers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpeakers_Speakers_SpeakerId",
                table: "EventSpeakers",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventTags_Events_EventId",
                table: "EventTags",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventTags_Tags_TagId",
                table: "EventTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_CourseCategories_CourseCategoryId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpeakers_Events_EventId",
                table: "EventSpeakers");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpeakers_Speakers_SpeakerId",
                table: "EventSpeakers");

            migrationBuilder.DropForeignKey(
                name: "FK_EventTags_Events_EventId",
                table: "EventTags");

            migrationBuilder.DropForeignKey(
                name: "FK_EventTags_Tags_TagId",
                table: "EventTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Speakers",
                table: "Speakers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventTags",
                table: "EventTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventSpeakers",
                table: "EventSpeakers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "Speakers",
                newName: "Speaker");

            migrationBuilder.RenameTable(
                name: "EventTags",
                newName: "EventTag");

            migrationBuilder.RenameTable(
                name: "EventSpeakers",
                newName: "EventSpeaker");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Event");

            migrationBuilder.RenameIndex(
                name: "IX_EventTags_TagId",
                table: "EventTag",
                newName: "IX_EventTag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_EventTags_EventId",
                table: "EventTag",
                newName: "IX_EventTag_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_EventSpeakers_SpeakerId",
                table: "EventSpeaker",
                newName: "IX_EventSpeaker_SpeakerId");

            migrationBuilder.RenameIndex(
                name: "IX_EventSpeakers_EventId",
                table: "EventSpeaker",
                newName: "IX_EventSpeaker_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_CourseCategoryId",
                table: "Event",
                newName: "IX_Event_CourseCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Speaker",
                table: "Speaker",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventTag",
                table: "EventTag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventSpeaker",
                table: "EventSpeaker",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_CourseCategories_CourseCategoryId",
                table: "Event",
                column: "CourseCategoryId",
                principalTable: "CourseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpeaker_Event_EventId",
                table: "EventSpeaker",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpeaker_Speaker_SpeakerId",
                table: "EventSpeaker",
                column: "SpeakerId",
                principalTable: "Speaker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventTag_Event_EventId",
                table: "EventTag",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventTag_Tags_TagId",
                table: "EventTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
