using Domain.Models;
using Domain.Responses.Responses_TicketType;

namespace UseCases.Mapper.Mapper_TicketType;

public static class GetTicketTypesAndShowsOfEventResponseMapper
{
    public static GetTicketTypesAndShowsOfEventResponse MapToGetTicketTypesAndShowsOfEventResponse(
        this TicketTypeShow ticketTypeShow)
        => new(
            Id: ticketTypeShow.TicketTypeId,
            TicketTypeName: ticketTypeShow.TicketType.Name,
            ShowId: ticketTypeShow.ShowId,
            ShowName: ticketTypeShow.Show.Title,
            StartsAt: ticketTypeShow.Show.StartsAt,
            EndsAt: ticketTypeShow.Show.EndsAt,
            Price: ticketTypeShow.TicketType.Price,
            Amount: ticketTypeShow.TicketType.Amount
            );
}