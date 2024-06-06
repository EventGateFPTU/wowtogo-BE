using Microsoft.OpenApi.Models;

namespace API.Endpoints;
public static class TicketEndpoints
{
    public static RouteGroupBuilder MapTicketEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("",() => "hello tickets");
        return group;
    }
}