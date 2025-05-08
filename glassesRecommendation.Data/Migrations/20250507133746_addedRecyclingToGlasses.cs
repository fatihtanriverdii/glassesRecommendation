using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace glassesRecommendation.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedRecyclingToGlasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRecycling",
                table: "Glasses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRecycling",
                table: "Glasses");
        }
    }
}
