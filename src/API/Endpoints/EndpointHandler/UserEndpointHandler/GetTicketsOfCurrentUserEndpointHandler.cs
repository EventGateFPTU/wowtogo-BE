using Ardalis.Result;
using Domain.Responses.Responses_Ticket;
using Domain.Responses.Shared;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using UseCases.UC_Ticket.Queries.GetTicketsOfCurrentUser;

namespace API.Endpoints.EndpointHandler.UserEndpointHandler;
public class GetTicketsOfCurrentUserEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, int pageNumber, int pageSize)
    {
        Result<PaginatedResponse<GetTicketDetailsResponse>> result = await sender.Send(new GetTicketsOfCurrentUserQuery(PageNumber: pageNumber,
                                                                                                                        PageSize: pageSize));
        if (!result.IsSuccess) return Results.NotFound();
        return Results.Ok(result);
    }
}