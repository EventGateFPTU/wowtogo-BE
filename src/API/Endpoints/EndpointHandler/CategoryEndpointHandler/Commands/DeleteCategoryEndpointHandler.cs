using Ardalis.Result;
using MediatR;
using UseCases.UC_Category.Commands.DeleteCategory;

namespace API.Endpoints.EndpointHandler.CategoryEndpointHandler.Commands
{
	public class DeleteCategoryEndpointHandler
	{
		public static async Task<Microsoft.AspNetCore.Http.IResult> Handle(ISender sender, Guid id, CancellationToken cancellationToken = default)
		{
			Result result = await sender.Send(new DeleteCategoryCommand(id), cancellationToken);
			if (!result.IsSuccess)
			{
				if (result.Status == ResultStatus.NotFound) return Results.NotFound(result);
				return Results.BadRequest(result);
			}
			return Results.NoContent();
		}
	}
}
