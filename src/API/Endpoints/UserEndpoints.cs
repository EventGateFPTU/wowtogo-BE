using Microsoft.OpenApi.Models;

namespace API.Endpoints;
public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("", () => "hello users");
        return group;
    }
}