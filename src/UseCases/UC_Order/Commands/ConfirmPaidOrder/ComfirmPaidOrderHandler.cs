using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Ticket;
using MediatR;
using UseCases.UC_Ticket.Commands.CreateTicket;

namespace UseCases.UC_Order.Commands.ConfirmPaidOrder;
public class ComfirmPaidOrderHandler(IUnitOfWork unitOfWork, ISender sender) : IRequestHandler<ConfirmPaidOrderCommand, Result<GetTicketDetailResponse>>
{
    public async Task<Result<GetTicketDetailResponse>> Handle(ConfirmPaidOrderCommand request, CancellationToken cancellationToken)
    {
        // Check if the order is existed
        Order? order = await unitOfWork.OrderRepository.FindAsync(o => o.Id.Equals(request.OrderId), cancellationToken: cancellationToken, trackChanges: true);
        if (order is null) return Result.Error("Order is not found");
        // Check if the user is valid
        User? user = await unitOfWork.UserRepository.FindAsync(u => u.Id.Equals(order.UserId), cancellationToken: cancellationToken);
        if (user is null) return Result.Error("User not found");
        // Check if the event is valid
        Event? checkingEvent = await unitOfWork.TicketTypeRepository.GetEventFromTicketTypeIdAsync(order.TicketTypeId, cancellationToken);
        if (checkingEvent is null) return Result.Error("Event not found");
        // if (checkingEvent.StartsAt < DateTime.Now) return Result.Error("Event has already started");
        // Check if the user is an attendee of the event
        Attendee? attendee = await unitOfWork.AttendeeRepository
            .FindAsync(a => a.UserId.Equals(order.UserId) && a.EventId.Equals(checkingEvent.Id), cancellationToken: cancellationToken);
        if (attendee is null) return Result.Error("Attendee is not found");
        order.AcceptOrder();
        // Create ticket
        Result<GetTicketDetailResponse> ticketResult = await sender.Send(new CreateTicketCommand(order.TicketTypeId, attendee.Id), cancellationToken);
        if (ticketResult.Status != ResultStatus.Ok) return Result.Error("Failed to create ticket");
        return Result.Success(ticketResult, "Order is confirmed and ticket is created successfully");
    }
}