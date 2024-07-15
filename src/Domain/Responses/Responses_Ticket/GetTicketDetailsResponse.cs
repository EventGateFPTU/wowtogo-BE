namespace Domain.Responses.Responses_Ticket;
public record GetTicketDetailsResponse(Guid Id,
                            Guid AttendeeId,
                            string TicketType,
                            string EventName,
                            string Code,
                            DateTimeOffset CreatedAt);