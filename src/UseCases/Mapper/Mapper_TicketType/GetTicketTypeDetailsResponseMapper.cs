using Domain.Models;
using Domain.Responses.Responses_Show;
using Domain.Responses.Responses_TicketType;

namespace UseCases.Mapper.Mapper_TicketType;
public static class GetTicketTypeDetailsResponseMapper
{
  public static GetTicketTypeDetailsResponse MapToGetTicketTypeDetailsResponse(this TicketType ticketType)
      => new(
            Id: ticketType.Id,
            Shows: ticketType.TicketTypeShows.Select(tts => new GetShowDetailResponse(
                Id: tts.Show.Id,
                ShowTitle: tts.Show.Title,
                EventTitle: tts.Show.Title,
                StartsAt: tts.Show.StartsAt,
                EndsAt: tts.Show.EndsAt
              )).ToArray(),
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