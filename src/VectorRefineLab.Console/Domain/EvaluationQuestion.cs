namespace VectorRefineLab.Console.Domain;

public sealed class EvaluationQuestion
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Question { get; set; } = string.Empty;

    public string? Language { get; set; }

    public string? Intent { get; set; }

    public string? Notes { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public List<ExpectedDocument> ExpectedDocuments { get; set; } = [];
}