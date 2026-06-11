namespace VectorRefineLab.Console.Domain;

public sealed class ExpectedDocument
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid EvaluationQuestionId { get; set; }

    public Guid? DocumentId { get; set; }

    public Guid? ChunkId { get; set; }

    public string? ExpectedTitleContains { get; set; }

    public string? ExpectedSection { get; set; }

    public int RelevanceGrade { get; set; } = 1;
}