using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Botwos.Weather.Infrastructure.Persistence.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "final_phrases",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    short_code = table.Column<string>(maxLength: 15, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    description = table.Column<string>(maxLength: 500, nullable: true),
                    text_format = table.Column<string>(maxLength: 500, nullable: false),
                    language = table.Column<string>(maxLength: 10, nullable: false),
                    begin_precipitation_mm_range = table.Column<double>(nullable: false),
                    end_precipitation_mm_range = table.Column<double>(nullable: false),
                    begin_cloud_percentage_range = table.Column<int>(nullable: false),
                    end_cloud_percentage_range = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_final_phrases", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "greetings",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    short_code = table.Column<string>(maxLength: 15, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    description = table.Column<string>(maxLength: 500, nullable: true),
                    text_format = table.Column<string>(maxLength: 500, nullable: false),
                    language = table.Column<string>(maxLength: 10, nullable: false),
                    kind = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_greetings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "initial_phrases",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    short_code = table.Column<string>(maxLength: 15, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    description = table.Column<string>(maxLength: 500, nullable: true),
                    text_format = table.Column<string>(maxLength: 500, nullable: false),
                    language = table.Column<string>(maxLength: 10, nullable: false),
                    begin_feels_like_celsius_range = table.Column<double>(nullable: false),
                    end_feels_like_celsius_range = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_initial_phrases", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_uq_greetings_short_code",
                table: "greetings",
                columns: new[] { "short_code", "kind", "language" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "final_phrases");

            migrationBuilder.DropTable(
                name: "greetings");

            migrationBuilder.DropTable(
                name: "initial_phrases");
        }
    }
}
