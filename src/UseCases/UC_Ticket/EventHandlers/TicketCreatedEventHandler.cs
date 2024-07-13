using Domain.Events.Tickets;
using MediatR;
using UseCases.Common.Constants;
using UseCases.Common.Contracts;

namespace UseCases.UC_Ticket.EventHandlers;

public class TicketCreatedEventHandler(IPermissionManager permissionManager) : INotificationHandler<TicketCreatedEvent>
{
    public async Task Handle(TicketCreatedEvent notification, CancellationToken cancellationToken)
    {
        var ticketObj = $"ticket:{notification.TicketId.ToString()}";
        var ticketTypeObj = $"ticket_type:{notification.TicketTypeId.ToString()}";
        var ticketType = (ticketTypeObj, Relations.TicketType, ticketObj);
        var ticketTypeTicket = (ticketObj, Relations.TicketTypeTicket, ticketTypeObj);
        await permissionManager.PutPermission(ticketType,ticketTypeTicket);
    }
}