using Domain.Events.Events;
using MediatR;
using UseCases.Common.Constants;
using UseCases.Common.Contracts;

namespace UseCases.UC_Event.EventHandlers;

public class EventCreatedEventHandler(IPermissionManager permissionManager) : INotificationHandler<EventCreatedEvent>
{
    public async Task Handle(EventCreatedEvent notification, CancellationToken cancellationToken)
    {
        var userObj = RelationObjects.User(notification.UserId.ToString());
        var eventObj = RelationObjects.Event(notification.EventId.ToString());

        await permissionManager.PutPermission((userObj, Relations.EventOrganizer, eventObj));
    }
}