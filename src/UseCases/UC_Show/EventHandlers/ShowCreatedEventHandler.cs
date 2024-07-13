using Domain.Events.Shows;
using MediatR;
using UseCases.Common.Contracts;

namespace UseCases.UC_Show.EventHandlers;

public class ShowCreatedEventHandler(IPermissionManager permissionManager) : INotificationHandler<ShowCreatedEvent>
{
    public Task Handle(ShowCreatedEvent notification, CancellationToken cancellationToken)
    {
        
    }
}