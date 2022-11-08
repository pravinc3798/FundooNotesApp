using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class LabelEntityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LabelTable",
                columns: table => new
                {
                    LabelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelName = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelTable", x => x.LabelId);
                    table.ForeignKey(
                        name: "FK_LabelTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "NoteLabelTable",
                columns: table => new
                {
                    NLID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelId = table.Column<long>(nullable: false),
                    NoteId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteLabelTable", x => x.NLID);
                    table.ForeignKey(
                        name: "FK_NoteLabelTable_LabelTable_LabelId",
                        column: x => x.LabelId,
                        principalTable: "LabelTable",
                        principalColumn: "LabelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteLabelTable_NoteTable_NoteId",
                        column: x => x.NoteId,
                        principalTable: "NoteTable",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabelTable_UserId",
                table: "LabelTable",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteLabelTable_LabelId",
                table: "NoteLabelTable",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteLabelTable_NoteId",
                table: "NoteLabelTable",
                column: "NoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteLabelTable");

            migrationBuilder.DropTable(
                name: "LabelTable");
        }
    }
}
