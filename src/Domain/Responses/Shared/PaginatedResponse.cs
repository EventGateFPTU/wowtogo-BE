namespace Domain.Responses.Shared;
public record PaginatedResponse<T>(
    IEnumerable<T> Data,
    int PageNumber,
    int PageSize,
    int Count
) where T : class;