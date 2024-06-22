namespace Domain.Responses.Responses_Ticket;
public record GetTicketDetailsResponse(Guid Id,
                            Guid AttendeeId,
                            string TicketType,
                            string EventName,
                            string Code,
                            string? UsedInFormat,
                            DateTimeOffset CreatedAt,
                            DateTimeOffset? UsedAt);