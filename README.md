# VectorRefineLab

VectorRefineLab is a .NET laboratory for studying vector search, hybrid retrieval, reranking, Maximal Marginal Relevance (MMR), and context evaluation for Retrieval-Augmented Generation (RAG) applications.

The project focuses on understanding how to reduce noise in vector search results by experimenting with metadata filters, result refinement strategies, evaluation questions, and comparisons between retrieval approaches.

## Project Goals

- Study vector search with SQL Server.
- Compare retrieval strategies.
- Understand and reduce noise in top K results.
- Record search sessions and returned results.
- Create an evaluation base with expected questions and documents.
- Evolve toward reranking, MMR, and retrieval metrics.

## Current Stack

- .NET 10
- Console Application
- SQL Server
- Entity Framework Core
- EF Core Migrations

## Initial Project Structure

```text
VectorRefineLab/
+-- src/
|   `-- VectorRefineLab.Console/
|       +-- Domain/
|       +-- Infrastructure/
|       |   `-- Data/
|       +-- Migrations/
|       +-- Program.cs
|       `-- appsettings.json
+-- README.md
`-- LICENSE
```

## Main Entities

- `Document`: the original indexed document.
- `DocumentChunk`: a searchable section extracted from a document.
- `EmbeddingModel`: the model used to generate embeddings.
- `SearchSession`: one execution of a search.
- `SearchResult`: a result returned by a search session.
- `EvaluationQuestion`: a test question used for evaluation.
- `ExpectedDocument`: an expected document or section for an evaluation question.

## Database

The database schema is generated and versioned with EF Core Migrations.

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

In the future, the project may use native vector capabilities in SQL Server to store embeddings and execute similarity search.

## How to Run

```bash
dotnet restore
dotnet build
dotnet run --project ./src/VectorRefineLab.Console
```

## Roadmap

- Implement indexing for sample documents.
- Create a chunking service.
- Generate embeddings.
- Persist embeddings in SQL Server.
- Implement top K vector search.
- Implement reranking.
- Implement MMR.
- Create evaluation with metrics such as Recall@K, Precision@K, and MRR.
- Compare modes: vector only, vector + filter, vector + rerank, vector + rerank + MMR.

## Project Status

VectorRefineLab is in an early stage. The first check-in focuses on the base structure, domain entities, persistence infrastructure, and the initial database setup.

## License

This project uses the MIT License.
