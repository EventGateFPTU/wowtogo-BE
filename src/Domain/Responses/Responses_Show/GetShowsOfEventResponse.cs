namespace Domain.Responses.Responses_Show;
public record GetShowsOfEventResponse(
    int pageNumber,
    int pageSize,
    IEnumerable<GetShowDetailResponse> Shows
);