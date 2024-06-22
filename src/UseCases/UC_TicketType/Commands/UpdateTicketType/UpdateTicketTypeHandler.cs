using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;

namespace UseCases.UC_TicketType.Commands.UpdateTicketType;
public class UpdateTicketTypeHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateTicketTypeCommand, Result>
{
    public async Task<Result> Handle(UpdateTicketTypeCommand request, CancellationToken cancellationToken)
    {
        TicketType? checkingTicketType = await unitOfWork.TicketTypeRepository.FindAsync(tt => tt.Id.Equals(request.Id), trackChanges: true, cancellationToken: cancellationToken);
        if (checkingTicketType is null) return Result.NotFound("Ticket type not found");
        if (request.FromDate > request.ToDate) return Result.Error("From date should be less than to date");
        if (request.Amount < 0) return Result.Error("Amount should be greater than 0");
        if (request.LeastAmountBuy < 0) return Result.Error("Least amount buy should be greater than 0");
        if (request.MostAmountBuy < 0) return Result.Error("Most amount buy should be greater than 0");
        if (request.LeastAmountBuy > request.MostAmountBuy) return Result.Error("Least amount buy should be less than most amount buy");
        if (request.Price < 0) return Result.Error("Price should be greater than 0");
        if (request.Amount < request.LeastAmountBuy) return Result.Error("Amount should be greater than least amount buy");
        if (request.Amount < request.MostAmountBuy) return Result.Error("Amount should be greater than most amount buy");
        {
            checkingTicketType.ShowId = request.showId;
            checkingTicketType.Name = request.Name;
            checkingTicketType.Description = request.Description;
            checkingTicketType.ImageUrl = request.ImageUrl;
            checkingTicketType.Price = request.Price;
            checkingTicketType.FromDate = request.FromDate;
            checkingTicketType.ToDate = request.ToDate;
            checkingTicketType.Amount = request.Amount;
            checkingTicketType.LeastAmountBuy = request.LeastAmountBuy;
            checkingTicketType.MostAmountBuy = request.MostAmountBuy;
            checkingTicketType.UpdatedAt = DateTimeOffset.UtcNow;
        }
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to update ticket type");
        return Result.Success();
    }
}