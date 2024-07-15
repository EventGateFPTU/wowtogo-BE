using Domain.Responses.Responses_Attendee;

namespace Domain.Responses.Responses_Checkin;
public record GetCheckinDetailResponse(
    Guid Id,
    Guid ShowId,
    string ShowTitle,
    Guid TicketId,
    string TicketTypeName,
    string UsedInFormat,
    DateTimeOffset UsedAt,
    AttendeeDetailResponse Attendee
);