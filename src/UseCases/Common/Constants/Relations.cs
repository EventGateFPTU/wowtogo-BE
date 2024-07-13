namespace UseCases.Common.Constants;

public static class Relations
{
    // Event
    public const string EventOrganizer = "organizer";
    public const string EventStaff = "staff";
    
    // Show
    public const string AllowedTicketType = "public const string";
    public const string ShowEvent = "event";
    public const string CanCheckInShow = "can_checkin";

    // Ticket type
    public const string TicketTypeAssignee = "assignee";
    public const string TicketTypeEvent = "event";
    public const string TicketTypeTicket = "ticket";

    // Ticket
    public const string TicketType = "type";
    public const string CanCheckInTicket = "can_checkin_ticket";
}

public static class RelationObjects
{
    public static string User(string id) => $"user:{id}";
    public static string Event(string id) => $"event:{id}";
    public static string Show(string id) => $"show:{id}";
    public static string TicketType(string id) => $"ticket_type:{id}";
    public static string Ticket(string id) => $"ticket:{id}";
}