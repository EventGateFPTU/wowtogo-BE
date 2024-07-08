using Domain.Models.Events;
using MediatR;
using UseCases.Common.Constants;
using UseCases.Common.Contracts;
using UseCases.Common.Models;

namespace UseCases.UC_Event.EventHandlers;

public class EventCreatedEventHandler(CurrentUser currentUser, IPermissionManager permissionManager) : INotificationHandler<EventCreatedEvent>
{
    public async Task Handle(EventCreatedEvent notification, CancellationToken cancellationToken)
    {
        var user = $"user:{currentUser.User.Id.ToString()}";

        var obj = $"event:{notification.EventId}";

        await permissionManager.PutPermission((user, Relations.Organizer, obj));
    }
}