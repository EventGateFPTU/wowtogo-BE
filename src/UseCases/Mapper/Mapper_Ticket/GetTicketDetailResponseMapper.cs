using Domain.Models;
using Domain.Responses.Responses_Ticket;

namespace UseCases.Mapper.Mapper_Ticket;
public static class GetTicketDetailResponseMapper
{
    public static GetTicketDetailsResponse MapToGetTicketDetailsResponse(this Ticket ticket)
        => new GetTicketDetailsResponse(
            Id: ticket.Id,
            AttendeeId: ticket.AttendeeId,
            TicketType: ticket.TicketType?.Name ?? string.Empty,
            EventName: ticket.TicketType?.TicketTypeShows.FirstOrDefault()?.Show.Title ?? string.Empty,
            Code: ticket.Code,
            CreatedAt: ticket.CreatedAt
        );
}