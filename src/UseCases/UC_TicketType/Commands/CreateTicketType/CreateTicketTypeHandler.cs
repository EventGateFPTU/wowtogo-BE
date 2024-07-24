using Ardalis.Result;
using Domain.Events.TicketTypes;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_TicketType;
using MediatR;
using UseCases.Mapper.Mapper_TicketType;

namespace UseCases.UC_TicketType.Commands.CreateTicketType;
public class CreateTicketTypeHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateTicketTypeCommand, Result<CreateTicketTypeResponse>>
{
    public async Task<Result<CreateTicketTypeResponse>> Handle(CreateTicketTypeCommand request, CancellationToken cancellationToken)
    {
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
        // check validation
        var (validResult, message) = ticketType.IsValid();
        if (!validResult) return Result.Error(message);
        unitOfWork.TicketTypeRepository.Add(ticketType);
        var ttEvent = new TicketTypeCreatedEvent(
           eventId: request.EventId,
           ticketTypeId: ticketTypeId
        );
        ticketType.AddDomainEvent(ttEvent);
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to create ticket type");
        return Result.Success(ticketType.MapToTicketTypeResponse(), "Ticket type is created successfully");
    }
}