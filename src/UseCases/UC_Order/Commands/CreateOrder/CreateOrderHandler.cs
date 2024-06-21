using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using UseCases.UC_Attendees.Commands.CreateAttendee;

namespace UseCases.UC_Order.Commands.CreateOrder;
public class CreateOrderHandler(IUnitOfWork unitOfWork, ISender sender) : IRequestHandler<CreateOrderQuery, Result>
{

    public async Task<Result> Handle(CreateOrderQuery request, CancellationToken cancellationToken)
    {
        // Check if the ticket type is not found
        TicketType? ticketType = await unitOfWork.TicketTypeRepository.FindAsync(tt => tt.Id.Equals(request.TicketTypeId), cancellationToken: cancellationToken);
        if (ticketType is null) return Result.Error("Ticket Type is not found");
        Event? checkingEvent = await unitOfWork.TicketTypeRepository.GetEventFromTicketTypeIdAsync(request.TicketTypeId, cancellationToken);
        if (checkingEvent is null) return Result.Error("Event is not found");
        // Check if the ticket type is out of stock
        IEnumerable<Order> orders = await unitOfWork.OrderRepository.FindManyAsync(o => o.TicketTypeId.Equals(request.TicketTypeId) && o.Status.Equals(Domain.Enums.OrderStatusEnum.Paid), cancellationToken: cancellationToken);
        if (orders.Count() >= ticketType.Amount) return Result.Error("TicketType is out of stock");
        // Check if the user is not found
        User? user = await unitOfWork.UserRepository.FindAsync(u => u.Id.Equals(request.UserId), cancellationToken: cancellationToken);
        if (user is null) return Result.Error("User not found");
        Order order = new()
        {
            TicketTypeId = request.TicketTypeId,
            UserId = request.UserId,
            TotalPrice = ticketType.Price,
            Currency = request.Currency,
            Status = Domain.Enums.OrderStatusEnum.Pending,
        };
        unitOfWork.OrderRepository.Add(order);
        Result result = await sender.Send(new CreateAttendeeCommand(request.UserId, checkingEvent.Id, request.PhoneNumber, request.DateOfBirth), cancellationToken);
        if (!result.IsSuccess) return Result.Error("Failed to create attendee");
        // if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to create order");
        return Result.SuccessWithMessage("Create Order Successfully !");
    }
}