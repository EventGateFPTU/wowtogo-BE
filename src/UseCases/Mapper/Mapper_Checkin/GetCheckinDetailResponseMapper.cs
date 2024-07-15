using Domain.Models;
using Domain.Responses.Responses_Checkin;
using UseCases.Mapper.Mapp_Attendee;

namespace UseCases.Mapper.Mapper_Checkin;
public static class GetCheckinDetailResponseMapper
{
    public static GetCheckinDetailResponse MapToGetCheckinDetailResponse(this Checkin checkin)
        => new GetCheckinDetailResponse(
            Id: checkin.Id,
            ShowId: checkin.Show.Id,
            ShowTitle: checkin.Show.Title,
            TicketId: checkin.Ticket.Id,
            TicketTypeName: checkin.Ticket.TicketType.Name,
            UsedAt: checkin.UsedAt,
            UsedInFormat: checkin.UsedInFormat.ToString(),
            Attendee: checkin.Ticket.Attendee.MapToAttendeeDetailResponse(checkin.Ticket)
        );
}