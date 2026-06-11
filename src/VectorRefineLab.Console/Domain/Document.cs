namespace VectorRefineLab.Console.Domain;

public sealed class Document
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public string? SourceName { get; set; }

    public string? SourcePath { get; set; }

    public string? SourceType { get; set; }

    public string? Language { get; set; }

    public string? Domain { get; set; }

    public string? MetadataJson { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public List<DocumentChunk> Chunks { get; set; } = [];
}
