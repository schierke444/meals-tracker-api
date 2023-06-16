namespace BuildingBlocks.Commons.Models;

public record PaginatedResults<T>(IEnumerable<T> results, PageMetadata pagedata) where T : notnull;