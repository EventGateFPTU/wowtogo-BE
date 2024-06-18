using API.Endpoints.EndpointHandler;
namespace API.Endpoints;
public static class CategoryEndpoint
{
    public static RouteGroupBuilder MapCategoryEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", CategoryEndpointHandler.GetCategories);
        return group;
    }
}