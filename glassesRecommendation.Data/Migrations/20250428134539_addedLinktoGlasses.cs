﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace glassesRecommendation.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedLinktoGlasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Glasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Glasses");
        }
    }
}
