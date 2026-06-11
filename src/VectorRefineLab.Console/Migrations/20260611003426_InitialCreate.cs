using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VectorRefineLab.Console.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    SourceName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    SourcePath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SourceType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Language = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Domain = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MetadataJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmbeddingModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Dimensions = table.Column<int>(type: "int", nullable: false),
                    DistanceMetric = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmbeddingModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Intent = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Query = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    NormalizedQuery = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Mode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VectorTopK = table.Column<int>(type: "int", nullable: false),
                    FinalTopK = table.Column<int>(type: "int", nullable: false),
                    UseReranker = table.Column<bool>(type: "bit", nullable: false),
                    UseMmr = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentChunks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmbeddingModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChunkIndex = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentHash = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    TokenCount = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Section = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TagsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetadataJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentChunks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentChunks_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpectedDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EvaluationQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChunkId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExpectedTitleContains = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ExpectedSection = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    RelevanceGrade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpectedDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpectedDocuments_EvaluationQuestions_EvaluationQuestionId",
                        column: x => x.EvaluationQuestionId,
                        principalTable: "EvaluationQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SearchResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SearchSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChunkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VectorRank = table.Column<int>(type: "int", nullable: false),
                    VectorDistance = table.Column<double>(type: "float", nullable: false),
                    VectorScore = table.Column<double>(type: "float", nullable: true),
                    RerankRank = table.Column<int>(type: "int", nullable: true),
                    RerankScore = table.Column<double>(type: "float", nullable: true),
                    FinalRank = table.Column<int>(type: "int", nullable: true),
                    SelectedForContext = table.Column<bool>(type: "bit", nullable: false),
                    SelectionReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchResults_SearchSessions_SearchSessionId",
                        column: x => x.SearchSessionId,
                        principalTable: "SearchSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentChunks_DocumentId",
                table: "DocumentChunks",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentChunks_DocumentId_ContentHash",
                table: "DocumentChunks",
                columns: new[] { "DocumentId", "ContentHash" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentChunks_EmbeddingModelId",
                table: "DocumentChunks",
                column: "EmbeddingModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentChunks_Language",
                table: "DocumentChunks",
                column: "Language");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentChunks_Section",
                table: "DocumentChunks",
                column: "Section");

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingModels_Provider_Name_Dimensions",
                table: "EmbeddingModels",
                columns: new[] { "Provider", "Name", "Dimensions" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpectedDocuments_EvaluationQuestionId",
                table: "ExpectedDocuments",
                column: "EvaluationQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchResults_ChunkId",
                table: "SearchResults",
                column: "ChunkId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchResults_SearchSessionId",
                table: "SearchResults",
                column: "SearchSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchResults_SearchSessionId_FinalRank",
                table: "SearchResults",
                columns: new[] { "SearchSessionId", "FinalRank" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentChunks");

            migrationBuilder.DropTable(
                name: "EmbeddingModels");

            migrationBuilder.DropTable(
                name: "ExpectedDocuments");

            migrationBuilder.DropTable(
                name: "SearchResults");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "EvaluationQuestions");

            migrationBuilder.DropTable(
                name: "SearchSessions");
        }
    }
}
