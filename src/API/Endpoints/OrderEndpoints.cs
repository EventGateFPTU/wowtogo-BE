using API.Endpoints.EndpointHandler.OrderEndpointHandler.Commands;
using Microsoft.OpenApi.Models;

namespace API.Endpoints;
public static class OrderEndpoints
{
    public static RouteGroupBuilder MapOrderEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("", CreateOrderEndpointHandler.Handle);
        group.MapPost("confirm-paid/{orderId}", ConfirmPaidOrderEndpointHandler.Handle);
        return group;
    }
}