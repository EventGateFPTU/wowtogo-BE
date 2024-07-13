using Domain.Events.Tickets;
using MediatR;
using UseCases.Common.Constants;
using UseCases.Common.Contracts;

namespace UseCases.UC_Ticket.EventHandlers;

public class TicketCreatedEventHandler(IPermissionManager permissionManager) : INotificationHandler<TicketCreatedEvent>
{
    public async Task Handle(TicketCreatedEvent notification, CancellationToken cancellationToken)
    {
        var user = $"ticket_type:{notification.TicketTypeId.ToString()}";
        var obj = $"ticket:{notification.TicketId.ToString()}";
        await permissionManager.PutPermission((user, Relations.TicketType, obj));
    }
}