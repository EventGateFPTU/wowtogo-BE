using API.Endpoints.EndpointHandler.CategoryEndpointHandler.Commands;
using API.Endpoints.EndpointHandler.CategoryEndpointHandler.Queries;
using Swashbuckle.AspNetCore.Annotations;
namespace API.Endpoints;
public static class CategoryEndpoint
{
	public static RouteGroupBuilder MapCategoryEndpoints(this RouteGroupBuilder group)
	{
		group.MapGet("/", GetCategoriesEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Get all categories"));
		//POST
		group.MapPost("", CreateCategoryEndpointHandler.Handle)
			.WithMetadata(new SwaggerOperationAttribute("Create a category"))
			.RequireAuthorization();
		//DELETE
		group.MapDelete("/{id}", DeleteCategoryEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Delete a category"));
		//PUT
		group.MapPut("/{id}", UpdateCategoryEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Update a category"));
		return group;
	}
}