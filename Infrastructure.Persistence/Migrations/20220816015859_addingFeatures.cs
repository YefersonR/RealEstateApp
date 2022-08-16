﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class addingFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estates_Features_FeatureId",
                table: "Estates");

            migrationBuilder.DropIndex(
                name: "IX_Estates_FeatureId",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "FeatureId",
                table: "Estates");

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

            migrationBuilder.CreateTable(
                name: "FeaturesRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    EstateId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturesRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeaturesRelations_Estates_EstateId",
                        column: x => x.EstateId,
                        principalTable: "Estates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FeaturesRelations_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstateFeature_FeaturesIdsId",
                table: "EstateFeature",
                column: "FeaturesIdsId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesRelations_EstateId",
                table: "FeaturesRelations",
                column: "EstateId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesRelations_FeatureId",
                table: "FeaturesRelations",
                column: "FeatureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstateFeature");

            migrationBuilder.DropTable(
                name: "FeaturesRelations");

            migrationBuilder.AddColumn<int>(
                name: "FeatureId",
                table: "Estates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estates_FeatureId",
                table: "Estates",
                column: "FeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_Features_FeatureId",
                table: "Estates",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
