using Ardalis.Result;
using Domain.Responses.Responses_TicketType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UseCases.UC_Ticket.Commands.CreateTicket;
using UseCases.UC_TicketType.Commands.CreateTicketType;

namespace API.Endpoints.EndpointHandler.TicketTypeEndpointHandler.Commands;
public class CreateTicketTypeEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, [FromBody] CreateTicketTypeRequest request, CancellationToken cancellationToken = default)
    {
        Result<CreateTicketTypeResponse> result = await sender.Send(new CreateTicketTypeCommand(request.ShowId,
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
        return Results.Created("", result);
    }
    public record CreateTicketTypeRequest(Guid[] ShowId,
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