using Ardalis.Result;
using MediatR;
using UseCases.UC_Category.Commands.UpdateCategory;

namespace API.Endpoints.EndpointHandler.CategoryEndpointHandler.Commands
{
	public class UpdateCategoryEndpointHandler
	{
		public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, UpdateCategoryRequest request, CancellationToken cancellationToken = default)
		{
			Result result = await sender.Send(new UpdateCategoryCommand(request.Id, request.Name), cancellationToken);
			if (!result.IsSuccess)
			{
				if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
				return Results.BadRequest(result);
			}
			return Results.NoContent();
		}
	}
	public record UpdateCategoryRequest(Guid Id, string Name);
}
