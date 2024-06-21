using API.Endpoints.EndpointHandler.OrderEndpointHandler.Commands;
using API.Endpoints.EndpointHandler.OrderEndpointHandler.Queries;
using Microsoft.OpenApi.Models;

namespace API.Endpoints;
public static class OrderEndpoints
{
    public static RouteGroupBuilder MapOrderEndpoints(this RouteGroupBuilder group)
    {
        // Get Methods
        group.MapGet("pending/{userId}", GetPendingOrderEndpointHandler.Handle);
        // Post Methods
        group.MapPost("", CreateOrderEndpointHandler.Handle);
        group.MapPost("confirm-paid/{orderId}", ConfirmPaidOrderEndpointHandler.Handle);
        return group;
    }
}