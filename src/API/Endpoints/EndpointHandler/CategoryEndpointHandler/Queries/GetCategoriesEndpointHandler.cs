using Ardalis.Result;
using Domain.Responses.Responses_Category;
using MediatR;
using UseCases.UC_Category.Queries.GetCategories;

namespace API.Endpoints.EndpointHandler.CategoryEndpointHandler.Queries;
public class GetCategoriesEndpointHandler
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender,
                                                                        int pageNumber = 1,
                                                                        int pageSize = 10)
    {
        Result<GetCategoriesResponse> result = await sender.Send(new GetCategoriesQuery(PageNumber: pageNumber,
                                                                                         PageSize: pageSize));
        if (!result.IsSuccess)
        {
            if(result.Status == ResultStatus.NotFound)
                return Results.NotFound(result);
                return Results.BadRequest(result);
        }
        return Results.Ok(result);
    }
}