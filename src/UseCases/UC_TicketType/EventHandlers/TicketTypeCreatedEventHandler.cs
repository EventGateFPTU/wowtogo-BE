using Domain.Events.Tickets;
using Domain.Events.TicketTypes;
using MediatR;
using UseCases.Common.Constants;
using UseCases.Common.Contracts;

namespace UseCases.UC_TicketType.EventHandlers;

public class TicketTypeCreatedEventHandler(IPermissionManager permissionManager) : INotificationHandler<TicketTypeCreatedEvent>
{
    public async Task Handle(TicketTypeCreatedEvent notification, CancellationToken cancellationToken)
    {
        var ttObj = RelationObjects.TicketType(notification.TicketTypeId.ToString());
        var eventObj = RelationObjects.Event(notification.EventId.ToString());
        await permissionManager.PutPermission((eventObj, Relations.TicketTypeEvent, ttObj));
    }
}