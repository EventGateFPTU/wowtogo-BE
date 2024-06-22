using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;

namespace UseCases.UC_TicketType.Commands.CreateTicketType;
public class CreateTicketTypeHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateTicketTypeCommand, Result>
{
    public async Task<Result> Handle(CreateTicketTypeCommand request, CancellationToken cancellationToken)
    {
        Show? show = await unitOfWork.ShowRepository.FindAsync(s => s.Id.Equals(request.showId));
        if (show is null) return Result.NotFound("Show is not found");
        if (request.FromDate > request.ToDate) return Result.Error("From date should be less than to date");
        if (request.Amount < 0) return Result.Error("Amount should be greater than 0");
        if (request.LeastAmountBuy < 0) return Result.Error("Least amount buy should be greater than 0");
        if (request.MostAmountBuy < 0) return Result.Error("Most amount buy should be greater than 0");
        if (request.LeastAmountBuy > request.MostAmountBuy) return Result.Error("Least amount buy should be less than most amount buy");
        if (request.Price < 0) return Result.Error("Price should be greater than 0");
        if (request.Amount < request.LeastAmountBuy) return Result.Error("Amount should be greater than least amount buy");
        if (request.Amount < request.MostAmountBuy) return Result.Error("Amount should be greater than most amount buy");
        TicketType ticketType = new()
        {
            ShowId = request.showId,
            Name = request.Name,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            Price = request.Price,
            FromDate = request.FromDate,
            ToDate = request.ToDate,
            Amount = request.Amount,
            LeastAmountBuy = request.LeastAmountBuy,
            MostAmountBuy = request.MostAmountBuy,
            UpdatedAt = DateTime.UtcNow,
        };
        unitOfWork.TicketTypeRepository.Add(ticketType);
        if (!await unitOfWork.SaveChangesAsync()) return Result.Error("Failed to create ticket type");
        return Result.SuccessWithMessage("Ticket type is created successfully");
    }
}