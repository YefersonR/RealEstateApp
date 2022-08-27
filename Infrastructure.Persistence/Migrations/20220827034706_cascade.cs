using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeaturesRelations_Estates_EstateId",
                table: "FeaturesRelations");

            migrationBuilder.AddForeignKey(
                name: "FK_FeaturesRelations_Estates_EstateId",
                table: "FeaturesRelations",
                column: "EstateId",
                principalTable: "Estates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeaturesRelations_Estates_EstateId",
                table: "FeaturesRelations");

            migrationBuilder.AddForeignKey(
                name: "FK_FeaturesRelations_Estates_EstateId",
                table: "FeaturesRelations",
                column: "EstateId",
                principalTable: "Estates",
                principalColumn: "Id");
        }
    }
}
