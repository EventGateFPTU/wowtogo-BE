using Domain.Models;
using Domain.Responses.Responses_Ticket;

namespace UseCases.Mapper.Mapper_Ticket;
public static class GetTicketDetailResponseMapper
{
    public static GetTicketDetailResponse MapToGetTicketDetailResponse(this Ticket ticket)
        => new GetTicketDetailResponse(
            Id: ticket.Id,
            AttendeeId: ticket.AttendeeId,
            TicketType: ticket.TicketType.Name,
            EventName: ticket.TicketType.Show.Event.Title,
            Code: ticket.Code,
            UsedInFormat: ticket.UsedInFormat.ToString(),
            CreatedAt: ticket.CreatedAt,
            UsedAt: ticket.UsedAt
        );
}