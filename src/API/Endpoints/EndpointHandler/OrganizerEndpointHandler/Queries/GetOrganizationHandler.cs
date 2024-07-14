using Ardalis.Result;
using Domain.Responses.Responses_Event;
using Domain.Responses.Responses_Organizer;
using MediatR;
using UseCases.UC_Event.Query.GetOrganizerEvents;
using UseCases.UC_Organizer.Queries.GetOrganization;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace API.Endpoints.EndpointHandler.OrganizerEndpointHandler.Queries;

public class GetOrganizationHandler
{
    public static async Task<IResult> Handle(ISender sender)
    {
        Result<OrganizerDB> result = await sender.Send(new GetOrganizationQuery());
        if (result.IsSuccess) return Results.Ok(result);
        if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
        return Results.BadRequest(result);
    }
}