using Domain.Events.Shows;
using MediatR;
using UseCases.Common.Constants;
using UseCases.Common.Contracts;

namespace UseCases.UC_Show.EventHandlers;

public class ShowCreatedEventHandler(IPermissionManager permissionManager) : INotificationHandler<ShowCreatedEvent>
{
    public async Task Handle(ShowCreatedEvent notification, CancellationToken cancellationToken)
    {
        var eventObj = RelationObjects.Event(notification.EventId.ToString());
        var showObj = RelationObjects.Show(notification.ShowId.ToString());

        var eventRelationTuple = (eventObj, Relations.ShowEvent, showObj);
        
        var tuples = notification.TicketTypeIds
            .Select(tt => (
                RelationObjects.TicketType(tt.ToString()),
                Relations.AllowedTicketType,
                showObj.ToString()
                ))
            .Append(eventRelationTuple)
            .ToArray();
        await permissionManager.PutPermission(tuples);
    }
}