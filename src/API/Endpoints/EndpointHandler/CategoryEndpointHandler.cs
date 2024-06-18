using Ardalis.Result;
using Domain.Responses.Responses_Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UseCases.UC_Category.Queries.GetCategories;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace API.Endpoints.EndpointHandler;
public static class CategoryEndpointHandler
{
    public static async Task<IResult> GetCategories(IMediator mediator,
                                                                            [FromQuery(Name = "pageNumber")] int PageNumber = 1,
                                                                            [FromQuery(Name = "pageSize")] int PageSize = 10)
    {
        Result<GetCategoriesResponse> result = await mediator.Send(new GetCategoriesQuery(PageNumber, PageSize));
        return Results.Ok(result);
    }
}