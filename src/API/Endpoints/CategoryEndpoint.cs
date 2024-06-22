using API.Endpoints.EndpointHandler;
using API.Endpoints.EndpointHandler.CategoryEndpointHandler.Queries;
using Swashbuckle.AspNetCore.Annotations;
namespace API.Endpoints;
public static class CategoryEndpoint
{
    public static RouteGroupBuilder MapCategoryEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetCategoriesEndpointHandler.Handle).WithMetadata(new SwaggerOperationAttribute("Get all categories"));
        return group;
    }
}