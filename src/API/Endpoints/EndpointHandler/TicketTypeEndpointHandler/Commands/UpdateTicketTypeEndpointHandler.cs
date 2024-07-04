using Ardalis.Result;
using MediatR;
using UseCases.UC_TicketType.Commands.UpdateTicketType;

namespace API.Endpoints.EndpointHandler.TicketTypeEndpointHandler.Commands;
public class UpdateTicketTypeEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid ticketTypeId, UpdateTicketTypeRequest request, CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new UpdateTicketTypeCommand(ticketTypeId,
                                                                      request.ShowId,
                                                                      request.Name,
                                                                      request.Description,
                                                                      request.ImageUrl,
                                                                      request.Price,
                                                                      request.FromDate,
                                                                      request.ToDate,
                                                                      request.Amount,
                                                                      request.LeastAmountBuy,
                                                                      request.MostAmountBuy
                                                                      ), cancellationToken);
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.NoContent();
    }
    public record UpdateTicketTypeRequest(Guid[] ShowId,
                                           string Name,
                                           string Description,
                                           string ImageUrl,
                                           decimal Price,
                                           DateTimeOffset FromDate,
                                           DateTimeOffset ToDate,
                                           int Amount,
                                           int LeastAmountBuy,
                                           int MostAmountBuy
                                           );
}