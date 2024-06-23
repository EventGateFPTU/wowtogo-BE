using Domain.Models;
using Domain.Responses.Responses_TicketType;

namespace UseCases.Mapper.Mapper_TicketType;
public static class GetTicketTypeDetailsResponseMapper
{
  public static GetTicketTypeDetailsResponse MapToGetTicketTypeDetailsResponse(this TicketType ticketType)
      => new(
            Id: ticketType.Id,
              ShowTitle: ticketType.Show?.Title ?? string.Empty,
              Name: ticketType.Name,
              Description: ticketType.Description,
              ImageUrl: ticketType.ImageUrl,
              Price: ticketType.Price,
              FromDate: ticketType.FromDate,
              ToDate: ticketType.ToDate,
              Amount: ticketType.Amount,
              LeastAmountBuy: ticketType.LeastAmountBuy,
              MostAmountBuy: ticketType.MostAmountBuy,
              CreatedAt: ticketType.CreatedAt
      );
}