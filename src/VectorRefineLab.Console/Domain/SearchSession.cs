namespace VectorRefineLab.Console.Domain;

public sealed class SearchSession
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Query { get; set; } = string.Empty;

    public string? NormalizedQuery { get; set; }

    public string Mode { get; set; } = string.Empty;

    public int VectorTopK { get; set; }

    public int FinalTopK { get; set; }

    public bool UseReranker { get; set; }

    public bool UseMmr { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public List<SearchResult> Results { get; set; } = [];
}