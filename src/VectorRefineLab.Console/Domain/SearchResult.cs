namespace VectorRefineLab.Console.Domain;

public sealed class SearchResult
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid SearchSessionId { get; set; }

    public Guid DocumentId { get; set; }

    public Guid ChunkId { get; set; }

    public int VectorRank { get; set; }

    public double VectorDistance { get; set; }

    public double? VectorScore { get; set; }

    public int? RerankRank { get; set; }

    public double? RerankScore { get; set; }

    public int? FinalRank { get; set; }

    public bool SelectedForContext { get; set; }

    public string? SelectionReason { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}