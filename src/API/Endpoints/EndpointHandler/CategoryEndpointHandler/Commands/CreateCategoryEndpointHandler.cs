using Ardalis.Result;
using MediatR;
using UseCases.UC_Category.Commands.CreateCategory;

namespace API.Endpoints.EndpointHandler.CategoryEndpointHandler.Commands
{
	public class CreateCategoryEndpointHandler
	{
		public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, CreateCategoryRequest request, CancellationToken cancellationToken = default)
		{
			Result result = await sender.Send(new CreateCategoryCommand(request.name), cancellationToken);
			if (!result.IsSuccess)
			{
				if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
				return Results.BadRequest(result);
			}
			return Results.Created();
		}
	}
	public record CreateCategoryRequest(string name);
}
