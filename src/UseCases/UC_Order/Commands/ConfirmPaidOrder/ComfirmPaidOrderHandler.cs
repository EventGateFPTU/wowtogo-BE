using Ardalis.Result;
using Domain.Enums;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Ticket;
using MediatR;
using UseCases.UC_Ticket.Commands.CreateTicket;

namespace UseCases.UC_Order.Commands.ConfirmPaidOrder;
public class ComfirmPaidOrderHandler(IUnitOfWork unitOfWork, ISender sender) : IRequestHandler<ConfirmPaidOrderCommand, Result<CreateTicketResponse>>
{
    public async Task<Result<CreateTicketResponse>> Handle(ConfirmPaidOrderCommand request, CancellationToken cancellationToken)
    {
        // Check if the order is existed
        Order? order = await unitOfWork.OrderRepository.FindAsync(o => o.Id.Equals(request.OrderId), cancellationToken: cancellationToken, trackChanges: true);
        if (order is null) return Result.NotFound("Order is not found");
        // Check if the user is valid
        User? user = await unitOfWork.UserRepository.FindAsync(u => u.Id.Equals(order.UserId), cancellationToken: cancellationToken);
        if (user is null) return Result.NotFound("User not found");
        // TODO: Check if user is the ticket owner

        if (order.Status != OrderStatusEnum.Pending)
            return Result.Error("Order should be pending");
        // Check if the event is valid
        Event? checkingEvent = await unitOfWork.TicketTypeRepository.GetEventFromTicketTypeIdAsync(order.TicketTypeId, cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event not found");
        Attendee? attendee = await unitOfWork.AttendeeRepository
            .FindAsync(a => a.UserId.Equals(order.UserId) && a.EventId.Equals(checkingEvent.Id), cancellationToken: cancellationToken);
        if (attendee is null) return Result.NotFound("Attendee is not found");
        order.AcceptOrder();
        // Create ticket
        Result<CreateTicketResponse> ticketResult = await sender.Send(new CreateTicketCommand(order.TicketTypeId, attendee.Id), cancellationToken);
        if (ticketResult.Status != ResultStatus.Ok) return Result.Error("Failed to create ticket");
        return Result.Success(ticketResult, "Order is confirmed and ticket is created successfully");
    }
}