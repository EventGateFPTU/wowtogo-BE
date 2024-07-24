using Ardalis.Result;
using Domain.Responses.Responses_TicketType;
using Domain.Responses.Shared;
using MediatR;
using UseCases.UC_TicketType.Queries.GetTicketTypeById;
using UseCases.UC_TicketType.Queries.GetTicketTypesOfShow;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace API.Endpoints.EndpointHandler.TicketTypeEndpointHandler.Queries;

public class GetTicketTypeByIdHandler
{
    public static async Task<IResult> Handle(ISender sender, Guid ticketTypeId, CancellationToken cancellationToken = default)
    {
        Result<GetTicketTypeByIdResponse> result = await sender.Send(new GetTicketTypeByIdQuery(ticketTypeId), cancellationToken);
        if (result.IsSuccess) return Results.Ok(result);
        if (result.Status == ResultStatus.NotFound)
            return Results.NotFound(result);
        return Results.BadRequest(result);
    }
}