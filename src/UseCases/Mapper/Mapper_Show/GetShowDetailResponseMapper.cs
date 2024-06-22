using Domain.Models;
using Domain.Responses.Responses_Show;

namespace UseCases.Mapper.Mapper_Show;
public static class GetShowsOfEventResponseMapper
{
    public static GetShowDetailResponse MapToGetShowDetailResponse(this Show show)
    {
        return new GetShowDetailResponse(
            Id: show.Id,
            ShowTitle: show.Title,
            EventTitle: show.Event?.Title ?? string.Empty,
            StartsAt: show.StartsAt,
            EndsAt: show.EndsAt
        );
    }
}