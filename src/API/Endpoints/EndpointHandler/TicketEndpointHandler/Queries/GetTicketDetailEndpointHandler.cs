using Ardalis.Result;
using Domain.Responses.Responses_Ticket;
using MediatR;
using UseCases.UC_Ticket.Queries.GetTicketDetail;

namespace API.Endpoints.EndpointHandler.TicketEndpointHandler.Queries;
public class GetTicketDetailEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender,
                                            Guid ticketId)
    {
        Result<GetTicketDetailsResponse> result = await sender.Send(new GetTicketDetailQuery(ticketId));
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.NotFound)
                return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.Ok(result);
    }
}