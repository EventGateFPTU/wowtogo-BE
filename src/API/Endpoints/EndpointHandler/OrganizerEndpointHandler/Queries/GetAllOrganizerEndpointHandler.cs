using Ardalis.Result;
using Domain.Responses.Responses_Organizer;
using Domain.Responses.Shared;
using MediatR;
using UseCases.UC_Organizer.Queries.GetAllOrganizer;

namespace API.Endpoints.EndpointHandler.OrganizerEndpointHandler.Queries
{
    public class GetAllOrganizerEndpointHandler
    {
        public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, int pageNumber = 1, int pageSize = 10, string? searchTerm = null)
        {
            Result<PaginatedResponse<OrganizerDB>> result = await sender.Send(new GetAllOrganizerQuery(
              PageNumber: pageNumber,
              PageSize: pageSize,
              SearchTerm: searchTerm
              ));
            if (!result.IsSuccess)
            {
                if (result.Status == ResultStatus.NotFound)
                    return Results.NotFound(result);
                return Results.BadRequest(result);
            }
            return Results.Ok(result);
        }
    }
}
