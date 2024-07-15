using Ardalis.Result;
using Domain.Responses.Responses_Checkin;
using Domain.Responses.Responses_Ticket;
using Domain.Responses.Shared;
using MediatR;
using UseCases.UC_Ticket.Query.GetCheckinsByEvent;

namespace API.Endpoints.EndpointHandler.EventEndpointHandler.Queries;
public class GetCheckinByEventEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid eventId, int pageSize = 10, int pageNumber = 1)
    {
        Result<PaginatedResponse<GetCheckinDetailResponse>> result = await sender.Send(new GetCheckinsByEventQuery(EventId: eventId,
                                                                                                                    PageSize: pageSize,
                                                                                                                    PageNumber: pageNumber));
        if (!result.IsSuccess) return Results.NotFound();
        return Results.Ok(result);
    }
}