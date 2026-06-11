namespace VectorRefineLab.Console.Domain;

public sealed class DocumentChunk
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid DocumentId { get; set; }
    public Guid EmbeddingModelId { get; set; }

    public int ChunkIndex { get; set; }

    public string Content { get; set; } = string.Empty;

    public string ContentHash { get; set; } = string.Empty;

    public int TokenCount { get; set; }

    public string? Language { get; set; }

    public string? Section { get; set; }

    public string? TagsJson { get; set; }

    public string? MetadataJson { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}