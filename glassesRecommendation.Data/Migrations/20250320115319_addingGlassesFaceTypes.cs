using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace glassesRecommendation.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingGlassesFaceTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FaceType",
                table: "Glasses",
                newName: "GlassesType");

            migrationBuilder.AddColumn<bool>(
                name: "Heart",
                table: "Glasses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Oblong",
                table: "Glasses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Oval",
                table: "Glasses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Round",
                table: "Glasses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Square",
                table: "Glasses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Heart",
                table: "Glasses");

            migrationBuilder.DropColumn(
                name: "Oblong",
                table: "Glasses");

            migrationBuilder.DropColumn(
                name: "Oval",
                table: "Glasses");

            migrationBuilder.DropColumn(
                name: "Round",
                table: "Glasses");

            migrationBuilder.DropColumn(
                name: "Square",
                table: "Glasses");

            migrationBuilder.RenameColumn(
                name: "GlassesType",
                table: "Glasses",
                newName: "FaceType");
        }
    }
}
