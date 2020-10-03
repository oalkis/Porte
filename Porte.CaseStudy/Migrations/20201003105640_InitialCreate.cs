using Microsoft.EntityFrameworkCore.Migrations;

namespace Porte.CaseStudy.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boxs",
                columns: table => new
                {
                    BOX_ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WEIGHT = table.Column<int>(nullable: false),
                    PART_COUNT = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxs", x => x.BOX_ID);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    PART_ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BOX_ID = table.Column<int>(nullable: false),
                    PART_NUMBER = table.Column<int>(nullable: false),
                    PART_WEIGHT = table.Column<int>(nullable: false),
                    PART_COST = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.PART_ID);
                });

            migrationBuilder.InsertData(
                table: "Boxs",
                columns: new[] { "BOX_ID", "PART_COUNT", "WEIGHT" },
                values: new object[] { 123450, 1, 3 });

            migrationBuilder.InsertData(
                table: "Boxs",
                columns: new[] { "BOX_ID", "PART_COUNT", "WEIGHT" },
                values: new object[] { 123461, 1, 8 });

            migrationBuilder.InsertData(
                table: "Boxs",
                columns: new[] { "BOX_ID", "PART_COUNT", "WEIGHT" },
                values: new object[] { 123472, 1, 11 });

            migrationBuilder.InsertData(
                table: "Boxs",
                columns: new[] { "BOX_ID", "PART_COUNT", "WEIGHT" },
                values: new object[] { 123483, 1, 3 });

            migrationBuilder.InsertData(
                table: "Boxs",
                columns: new[] { "BOX_ID", "PART_COUNT", "WEIGHT" },
                values: new object[] { 123494, 1, 13 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boxs");

            migrationBuilder.DropTable(
                name: "Parts");
        }
    }
}
