using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_TicketType;
using MediatR;

namespace UseCases.UC_TicketType.Queries.GetTicketTypeById;

public class GetTicketTypeByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTicketTypeByIdQuery, Result<GetTicketTypeByIdResponse>>
{
    public async Task<Result<GetTicketTypeByIdResponse>> Handle(GetTicketTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var ticketType = await unitOfWork.TicketTypeRepository
            .FindAsync(x => x.Id == request.TicketTypeId, cancellationToken: cancellationToken);

        if (ticketType is null) return Result.NotFound("Ticket type does not exist");
        var response = new GetTicketTypeByIdResponse(
            Id: ticketType.Id,
            Name: ticketType.Name,
            Description: ticketType.Description,
            ImageUrl: ticketType.ImageUrl,
            Price: ticketType.Price,
            FromDate: ticketType.FromDate,
            ToDate: ticketType.ToDate,
            Amount: ticketType.Amount,
            LeastAmountBuy: ticketType.LeastAmountBuy,
            MostAmountBuy: ticketType.MostAmountBuy);

        return Result.Success(response);
    }
}