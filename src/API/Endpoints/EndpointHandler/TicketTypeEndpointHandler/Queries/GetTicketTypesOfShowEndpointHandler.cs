using Ardalis.Result;
using Domain.Responses.Responses_TicketType;
using Domain.Responses.Shared;
using MediatR;
using UseCases.UC_TicketType.Queries.GetTicketTypesOfShow;

namespace API.Endpoints.EndpointHandler.TicketTypeEndpointHandler.Queries;
public class GetTicketTypesOfShowEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid showId, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        Result<PaginatedResponse<GetTicketTypeDetailsResponse>> result = await sender.Send(new GetTicketTypesOfShowQuery(
            ShowId: showId,
            PageNumber: pageNumber,
            PageSize: pageSize
            ), cancellationToken);
        if (!result.IsSuccess)
        {
            if (result.Status == ResultStatus.NotFound)
                return Results.NotFound(result);
            return Results.BadRequest(result);
        }
        return Results.Ok(result);
    }
}