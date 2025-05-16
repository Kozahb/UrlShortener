using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.Migrations
{
    /// <inheritdoc />
    public partial class AddExpirationDateToShortUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDateUtc",
                table: "ShortUrls",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDateUtc",
                table: "ShortUrls");
        }
    }
}
