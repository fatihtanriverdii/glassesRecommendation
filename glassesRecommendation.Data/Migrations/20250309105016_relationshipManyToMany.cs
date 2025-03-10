using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace glassesRecommendation.Data.Migrations
{
    /// <inheritdoc />
    public partial class relationshipManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlassesUser",
                columns: table => new
                {
                    GlassesId = table.Column<long>(type: "bigint", nullable: false),
                    UsersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlassesUser", x => new { x.GlassesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_GlassesUser_Glasses_GlassesId",
                        column: x => x.GlassesId,
                        principalTable: "Glasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GlassesUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GlassesUser_UsersId",
                table: "GlassesUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlassesUser");
        }
    }
}
