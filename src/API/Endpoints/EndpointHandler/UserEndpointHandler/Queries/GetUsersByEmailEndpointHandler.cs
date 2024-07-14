using MediatR;
using UseCases.UC_User.Queries.GetUsersByEmail;

namespace API.Endpoints.EndpointHandler.UserEndpointHandler.Queries;

public class GetUsersByEmailEndpointHandler
{
    public static async Task<IResult> Handle(ISender sender, string searchTerm, int pageNumber = 1,
        int pageSize = 7, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetUsersByEmailQuery(
            SearchTerm: searchTerm,
            PageSize: pageSize,
            PageNumber: pageNumber
            ), cancellationToken);
        if (result.IsSuccess) return Results.Ok(result);
        return Results.NotFound(result);
    }
}