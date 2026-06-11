using Microsoft.EntityFrameworkCore;
using VectorRefineLab.Console.Domain;

namespace VectorRefineLab.Console.Infrastructure.Data;

public sealed class VectorRefineDbContext : DbContext
{
    public VectorRefineDbContext(DbContextOptions<VectorRefineDbContext> options)
        : base(options)
    {
    }

    public DbSet<Document> Documents => Set<Document>();
    public DbSet<DocumentChunk> DocumentChunks => Set<DocumentChunk>();
    public DbSet<EmbeddingModel> EmbeddingModels => Set<EmbeddingModel>();
    public DbSet<SearchSession> SearchSessions => Set<SearchSession>();
    public DbSet<SearchResult> SearchResults => Set<SearchResult>();
    public DbSet<EvaluationQuestion> EvaluationQuestions => Set<EvaluationQuestion>();
    public DbSet<ExpectedDocument> ExpectedDocuments => Set<ExpectedDocument>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(entity =>
        {
            entity.ToTable("Documents");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Title)
                .HasMaxLength(300)
                .IsRequired();

            entity.Property(x => x.SourceName)
                .HasMaxLength(300);

            entity.Property(x => x.SourcePath)
                .HasMaxLength(1000);

            entity.Property(x => x.SourceType)
                .HasMaxLength(100);

            entity.Property(x => x.Language)
                .HasMaxLength(20);

            entity.Property(x => x.Domain)
                .HasMaxLength(100);

            entity.Property(x => x.MetadataJson)
                .HasColumnType("nvarchar(max)");

            entity.Property(x => x.CreatedAtUtc)
                .HasDefaultValueSql("SYSUTCDATETIME()");

            entity.HasMany(x => x.Chunks)
                .WithOne()
                .HasForeignKey(x => x.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<EmbeddingModel>(entity =>
        {
            entity.ToTable("EmbeddingModels");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Provider)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.DistanceMetric)
                .HasMaxLength(50);

            entity.Property(x => x.CreatedAtUtc)
                .HasDefaultValueSql("SYSUTCDATETIME()");

            entity.HasIndex(x => new { x.Provider, x.Name, x.Dimensions })
                .IsUnique();
        });

        modelBuilder.Entity<DocumentChunk>(entity =>
        {
            entity.ToTable("DocumentChunks");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Content)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            entity.Property(x => x.ContentHash)
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsRequired();

            entity.Property(x => x.Language)
                .HasMaxLength(20);

            entity.Property(x => x.Section)
                .HasMaxLength(150);

            entity.Property(x => x.TagsJson)
                .HasColumnType("nvarchar(max)");

            entity.Property(x => x.MetadataJson)
                .HasColumnType("nvarchar(max)");

            entity.Property(x => x.CreatedAtUtc)
                .HasDefaultValueSql("SYSUTCDATETIME()");

            // Inicialmente deixamos o embedding fora do domínio ou como string/binário.
            // A coluna VECTOR pode ser criada manualmente na migration.
            entity.Ignore("Embedding");

            entity.HasIndex(x => x.DocumentId);
            entity.HasIndex(x => x.EmbeddingModelId);
            entity.HasIndex(x => x.Language);
            entity.HasIndex(x => x.Section);

            entity.HasIndex(x => new { x.DocumentId, x.ContentHash })
                .IsUnique();
        });

        modelBuilder.Entity<SearchSession>(entity =>
        {
            entity.ToTable("SearchSessions");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Query)
                .HasMaxLength(1000)
                .IsRequired();

            entity.Property(x => x.NormalizedQuery)
                .HasMaxLength(1000);

            entity.Property(x => x.Mode)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .HasDefaultValueSql("SYSUTCDATETIME()");

            entity.HasMany(x => x.Results)
                .WithOne()
                .HasForeignKey(x => x.SearchSessionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SearchResult>(entity =>
        {
            entity.ToTable("SearchResults");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.SelectionReason)
                .HasMaxLength(500);

            entity.Property(x => x.CreatedAtUtc)
                .HasDefaultValueSql("SYSUTCDATETIME()");

            entity.HasIndex(x => x.SearchSessionId);
            entity.HasIndex(x => x.ChunkId);
            entity.HasIndex(x => new { x.SearchSessionId, x.FinalRank });
        });

        modelBuilder.Entity<EvaluationQuestion>(entity =>
        {
            entity.ToTable("EvaluationQuestions");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Question)
                .HasMaxLength(1000)
                .IsRequired();

            entity.Property(x => x.Language)
                .HasMaxLength(20);

            entity.Property(x => x.Intent)
                .HasMaxLength(100);

            entity.Property(x => x.Notes)
                .HasColumnType("nvarchar(max)");

            entity.Property(x => x.CreatedAtUtc)
                .HasDefaultValueSql("SYSUTCDATETIME()");

            entity.HasMany(x => x.ExpectedDocuments)
                .WithOne()
                .HasForeignKey(x => x.EvaluationQuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ExpectedDocument>(entity =>
        {
            entity.ToTable("ExpectedDocuments");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.ExpectedTitleContains)
                .HasMaxLength(300);

            entity.Property(x => x.ExpectedSection)
                .HasMaxLength(150);
        });
    }
}