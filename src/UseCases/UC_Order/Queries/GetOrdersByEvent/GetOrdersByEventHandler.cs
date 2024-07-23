using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Order;
using Domain.Responses.Shared;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Order.Queries.GetOrdersByEvent;
public class GetOrdersByEventHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<GetOrdersByEventQuery, Result<PaginatedResponse<OrderResponse>>>
{
    public async Task<Result<PaginatedResponse<OrderResponse>>> Handle(GetOrdersByEventQuery request, CancellationToken cancellationToken)
    {
        Event? checkingEvent = await unitOfWork.EventRepository.GetEventWithOrganizer(request.Id, cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        if (!IsCurrentUserOrganizerOrStaff(checkingEvent)) return Result.Forbidden();
        PaginatedResponse<OrderResponse> result = await unitOfWork.OrderRepository.GetOrdersByEventAsync(request.Id, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
        return Result.Success(result, "Get Order Successfully");
    }
    private bool IsCurrentUserOrganizerOrStaff(Event @event)
        => @event.Organizer.Id.Equals(currentUser.User!.Id) || @event.Staffs.Any(s => s.UserId.Equals(currentUser.User!.Id));
}