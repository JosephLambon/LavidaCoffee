using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LavidaCoffee.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEmailRequestModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailRequests",
                columns: table => new
                {
                    EmailRequestId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmailId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailRequests", x => x.EmailRequestId);
                    table.ForeignKey(
                        name: "FK_EmailRequests_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "EmailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailRequests_EmailId",
                table: "EmailRequests",
                column: "EmailId");
        }
    }
}
