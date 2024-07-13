using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Order;
using MediatR;
using UseCases.Common.Models;
using UseCases.Mapper.Mapper_Order;
using UseCases.UC_Attendees.Commands.CreateAttendee;

namespace UseCases.UC_Order.Commands.CreateOrder;
public class CreateOrderHandler(IUnitOfWork unitOfWork, ISender sender, CurrentUser currentUser) : IRequestHandler<CreateOrderCommand, Result<CreateOrderResponse>>
{

    public async Task<Result<CreateOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Guid currentUserId = currentUser.User!.Id;
        // Check if the ticket type is not found
        TicketType? ticketType = await unitOfWork.TicketTypeRepository.FindAsync(tt => tt.Id.Equals(request.TicketTypeId), cancellationToken: cancellationToken);
        if (ticketType is null) return Result.NotFound("Ticket Type is not found");
        Event? checkingEvent = await unitOfWork.TicketTypeRepository.GetEventFromTicketTypeIdAsync(request.TicketTypeId, cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        // Check if the ticket type is out of stock
        IEnumerable<Order> orders = await unitOfWork.OrderRepository.FindManyAsync(o => o.TicketTypeId.Equals(request.TicketTypeId) && o.Status.Equals(Domain.Enums.OrderStatusEnum.Paid), cancellationToken: cancellationToken);
        if (orders.Count() >= ticketType.Amount) return Result.Error("TicketType is out of stock");
        // Check if the user is not found
        User? user = await unitOfWork.UserRepository.FindAsync(u => u.Id.Equals(currentUserId), cancellationToken: cancellationToken);
        if (user is null) return Result.NotFound("User not found");
        Order order = new()
        {
            TicketTypeId = request.TicketTypeId,
            UserId = currentUserId,
            TotalPrice = ticketType.Price,
            Currency = request.Currency,
            Status = Domain.Enums.OrderStatusEnum.Pending,
        };
        unitOfWork.OrderRepository.Add(order);
        Result result = await sender.Send(new CreateAttendeeCommand(currentUserId, checkingEvent.Id, request.PhoneNumber, request.DateOfBirth), cancellationToken);
        if (!result.IsSuccess) return Result.Error("Failed to create attendee");
        // if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to create order");
        return Result.Success(order.MapToCreateOrderResponse(), "Create Order Successfully !");
    }
}