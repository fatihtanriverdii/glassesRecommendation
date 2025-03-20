using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace glassesRecommendation.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedGlassesTypeToFaceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GlassesType",
                table: "Glasses",
                newName: "FaceType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FaceType",
                table: "Glasses",
                newName: "GlassesType");
        }
    }
}
