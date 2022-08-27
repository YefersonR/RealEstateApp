using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class FeatureRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstateFeature");

            migrationBuilder.AddColumn<int>(
                name: "EstateId",
                table: "Features",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Features_EstateId",
                table: "Features",
                column: "EstateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Estates_EstateId",
                table: "Features",
                column: "EstateId",
                principalTable: "Estates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_Estates_EstateId",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Features_EstateId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "EstateId",
                table: "Features");

            migrationBuilder.CreateTable(
                name: "EstateFeature",
                columns: table => new
                {
                    EstatesId = table.Column<int>(type: "int", nullable: false),
                    FeaturesIdsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateFeature", x => new { x.EstatesId, x.FeaturesIdsId });
                    table.ForeignKey(
                        name: "FK_EstateFeature_Estates_EstatesId",
                        column: x => x.EstatesId,
                        principalTable: "Estates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstateFeature_Features_FeaturesIdsId",
                        column: x => x.FeaturesIdsId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstateFeature_FeaturesIdsId",
                table: "EstateFeature",
                column: "FeaturesIdsId");
        }
    }
}
