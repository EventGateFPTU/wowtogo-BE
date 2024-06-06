using Microsoft.OpenApi.Models;

namespace API.Endpoints;
public static class OrderEndpoints
{
    public static RouteGroupBuilder MapOrderEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("", () => "hello orders");
        return group;
    }
}