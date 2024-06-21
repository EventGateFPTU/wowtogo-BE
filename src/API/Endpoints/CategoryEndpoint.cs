using API.Endpoints.EndpointHandler;
using Swashbuckle.AspNetCore.Annotations;
namespace API.Endpoints;
public static class CategoryEndpoint
{
    public static RouteGroupBuilder MapCategoryEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", CategoryEndpointHandler.GetCategories).WithMetadata(new SwaggerOperationAttribute("Get all categories"));
        return group;
    }
}