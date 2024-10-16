using Ardalis.Result;
using Domain.Enums;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Ticket;
using MediatR;
using UseCases.Common.Models;
using UseCases.UC_Ticket.Commands.CreateTicket;

namespace UseCases.UC_Order.Commands.ConfirmPaidOrder;
public class ComfirmPaidOrderHandler(IUnitOfWork unitOfWork, ISender sender, CurrentUser currentUser) : IRequestHandler<ConfirmPaidOrderCommand, Result<CreateTicketResponse>>
{
    public async Task<Result<CreateTicketResponse>> Handle(ConfirmPaidOrderCommand request, CancellationToken cancellationToken)
    {
        // Check if the order is existed
        Order? order = await unitOfWork.OrderRepository.FindAsync(o => o.Id.Equals(request.OrderId), cancellationToken: cancellationToken, trackChanges: true);
        if (order is null) return Result.NotFound("Order is not found");
        // get ticket type
        TicketType? ticketType = await unitOfWork.TicketTypeRepository.FindAsync(tt => tt.Id.Equals(order.TicketTypeId), cancellationToken: cancellationToken, trackChanges: true);
        if (ticketType is null) return Result.NotFound("Ticket Type is not found");
        // if amount is out then refund order then return error
        if (ticketType.Amount <= 0)
        {
            order.Status = OrderStatusEnum.Refunded;
            if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Can not update order when refunded !");
            return Result.Error("Sold out !");
        }
        // Check if the user is valid
        // TODO: Check if user is the ticket owner
        // if (!IsCurrentUserOwnOrder(order)) return Result.Forbidden();
        // get user
        // User? user = await unitOfWork.UserRepository.FindAsync(u => u.Id.Equals(order.UserId), cancellationToken: cancellationToken);
        // if (user is null) return Result.NotFound("User not found");
        if (order.Status != OrderStatusEnum.Pending)
            return Result.Error("Order should be pending");
        // Check if the event is valid
        Event? checkingEvent = await unitOfWork.TicketTypeRepository.GetEventFromTicketTypeIdAsync(order.TicketTypeId, cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event not found");
        // get attendee object
        Attendee? attendee = await unitOfWork.AttendeeRepository
            .FindAsync(a => a.UserId.Equals(order.UserId) && a.EventId.Equals(checkingEvent.Id), cancellationToken: cancellationToken);
        if (attendee is null) return Result.NotFound("Attendee is not found");
        order.AcceptOrder();
        ticketType.ReduceOneAmount();
        // Create ticket
        Result<CreateTicketResponse> ticketResult = await sender.Send(new CreateTicketCommand(order.TicketTypeId, attendee.Id), cancellationToken);
        if (ticketResult.Status != ResultStatus.Ok) return Result.Error("Failed to create ticket");
        return Result.Success(ticketResult, "Order is confirmed and ticket is created successfully");
    }
    private bool IsCurrentUserOwnOrder(Order order)
        => order.UserId.Equals(currentUser.User!.Id);
}