namespace VectorRefineLab.Console.Domain;

public sealed class EmbeddingModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Provider { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int Dimensions { get; set; }

    public string? DistanceMetric { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}