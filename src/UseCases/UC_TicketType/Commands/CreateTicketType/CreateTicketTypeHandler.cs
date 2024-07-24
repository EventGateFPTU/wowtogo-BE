using Ardalis.Result;
using Domain.Events.TicketTypes;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_TicketType;
using MediatR;
using UseCases.Common.Models;
using UseCases.Mapper.Mapper_TicketType;

namespace UseCases.UC_TicketType.Commands.CreateTicketType;
public class CreateTicketTypeHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<CreateTicketTypeCommand, Result<CreateTicketTypeResponse>>
{
    public async Task<Result<CreateTicketTypeResponse>> Handle(CreateTicketTypeCommand request, CancellationToken cancellationToken)
    {
        // check validation for the ticket type
        if (request.FromDate > request.ToDate) return Result.Error("From date should be less than to date");
        if (request.Amount < 0) return Result.Error("Amount should be greater than 0");
        if (request.LeastAmountBuy < 0) return Result.Error("Least amount buy should be greater than 0");
        if (request.MostAmountBuy < 0) return Result.Error("Most amount buy should be greater than 0");
        if (request.LeastAmountBuy > request.MostAmountBuy) return Result.Error("Least amount buy should be less than most amount buy");
        if (request.Price < 0) return Result.Error("Price should be greater than 0");
        if (request.Amount < request.LeastAmountBuy) return Result.Error("Amount should be greater than least amount buy");
        if (request.Amount < request.MostAmountBuy) return Result.Error("Amount should be greater than most amount buy");
        Event? checkingEvent = await unitOfWork.EventRepository.GetEventWithOrganizer(request.EventId, cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        if (!IsCurrentUserOwnEvent(checkingEvent)) return Result.Forbidden();
        // new ticket type
        Guid ticketTypeId = Guid.NewGuid();
        TicketType ticketType = new()
        {
            Id = ticketTypeId,
            Name = request.Name,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            Price = request.Price,
            FromDate = request.FromDate,
            ToDate = request.ToDate,
            Amount = request.Amount,
            LeastAmountBuy = request.LeastAmountBuy,
            MostAmountBuy = request.MostAmountBuy,
            UpdatedAt = DateTimeOffset.UtcNow,
            EventId = request.EventId
        };
        unitOfWork.TicketTypeRepository.Add(ticketType);
        var ttEvent = new TicketTypeCreatedEvent(
           eventId: request.EventId,
           ticketTypeId: ticketTypeId
        );
        ticketType.AddDomainEvent(ttEvent);
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to create ticket type");
        return Result.Success(ticketType.MapToTicketTypeResponse(), "Ticket type is created successfully");
    }
    private bool IsCurrentUserOwnEvent(Event checkingEvent)
        => checkingEvent.Organizer.Id.Equals(currentUser.User!.Id);
}