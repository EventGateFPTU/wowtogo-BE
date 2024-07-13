using Domain.Models;
using Domain.Responses.Responses_TicketType;

namespace UseCases.Mapper.Mapper_TicketType;
public static class CreateTicketTypeResponseMapper
{
    public static CreateTicketTypeResponse MapToTicketTypeResponse(this TicketType ticketType)
        => new CreateTicketTypeResponse(
            Id: ticketType.Id,
            Name: ticketType.Name,
            Description: ticketType.Description,
            Price: ticketType.Price,
            FromDate: ticketType.FromDate,
            ToDate: ticketType.ToDate,
            Amount: ticketType.Amount,
            LeastAmountBuy: ticketType.LeastAmountBuy,
            MostAmountBuy: ticketType.MostAmountBuy,
            CreatedAt: ticketType.CreatedAt
        );
}