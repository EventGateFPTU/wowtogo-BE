using Ardalis.Result;
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
        if (request.showId.Count() > 0)
        {
            IEnumerable<Show> checkingShows = await unitOfWork.ShowRepository.FindManyAsync(s => request.showId.Contains(s.Id), cancellationToken: cancellationToken);
            if (checkingShows.Count() != request.showId.Count()) return Result.Error("Some shows are not found");
        }
        if (request.FromDate > request.ToDate) return Result.Error("From date should be less than to date");
        if (request.Amount < 0) return Result.Error("Amount should be greater than 0");
        if (request.LeastAmountBuy < 0) return Result.Error("Least amount buy should be greater than 0");
        if (request.MostAmountBuy < 0) return Result.Error("Most amount buy should be greater than 0");
        if (request.LeastAmountBuy > request.MostAmountBuy) return Result.Error("Least amount buy should be less than most amount buy");
        if (request.Price < 0) return Result.Error("Price should be greater than 0");
        if (request.Amount < request.LeastAmountBuy) return Result.Error("Amount should be greater than least amount buy");
        if (request.Amount < request.MostAmountBuy) return Result.Error("Amount should be greater than most amount buy");
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
            TicketTypeShows = request.showId.Select(showId => new TicketTypeShow
            {
                ShowId = showId,
                TicketTypeId = ticketTypeId
            }).ToList()
        };
        unitOfWork.TicketTypeRepository.Add(ticketType);
        if (!await unitOfWork.SaveChangesAsync()) return Result.Error("Failed to create ticket type");
        return Result.Success(ticketType.MapToTicketTypeResponse(), "Ticket type is created successfully");
    }
}