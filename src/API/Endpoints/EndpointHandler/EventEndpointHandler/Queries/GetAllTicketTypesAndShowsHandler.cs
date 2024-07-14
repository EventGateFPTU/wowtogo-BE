using Ardalis.Result;
using MediatR;
using UseCases.UC_TicketType.Queries.GetTicketTypesOfEvent;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Queries;

public class GetAllTicketTypesAndShowsHandler
{
    public static async Task<IResult> Handle(ISender sender,
        Guid eventId,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetTicketTypesOfEventQuery(
            EventId: eventId,
            PageNumber: pageNumber,
            PageSize: pageSize
        );
        var result = await sender.Send(query, cancellationToken);
        
        if (result.IsSuccess) return Results.Ok(result);
        if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
        return Results.BadRequest(result);
    }
}