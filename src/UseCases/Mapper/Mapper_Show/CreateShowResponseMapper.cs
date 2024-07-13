using Domain.Models;
using Domain.Responses.Responses_Show;

namespace UseCases.Mapper.Mapper_Show;
public static class CreateShowResponseMapper
{
    public static CreateShowResponse MapToCreateShowResponse(this Show show)
        => new CreateShowResponse(
            Id: show.Id,
            TicketTypeIds: show.TicketTypeShow.Select(tts => tts.TicketTypeId).ToArray(),
            EventId: show.EventId,
            Title: show.Title,
            StartsAt: show.StartsAt,
            EndsAt: show.EndsAt,
            CreatedAt: show.CreatedAt
        );
}