namespace Domain.Responses.Responses_Ticket;
public record CreateTicketResponse(Guid Id,
                            Guid AttendeeId,
                            string TicketType,
                            string EventName,
                            string Code,
                            DateTimeOffset CreatedAt);