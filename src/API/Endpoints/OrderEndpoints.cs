using API.Endpoints.EndpointHandler.OrderEndpointHandler.Commands;
using API.Endpoints.EndpointHandler.OrderEndpointHandler.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints;
public static class OrderEndpoints
{
    public static RouteGroupBuilder MapOrderEndpoints(this RouteGroupBuilder group)
    {
        // Get Methods
        group.MapGet("pending/{userId}", GetPendingOrderEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Get pending orders of a users"))
            .RequireAuthorization();
        group.MapGet("paid/{userId}", GetPaidOrderEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Get paid orders of a users"))
            .RequireAuthorization();
        // Post Methods
        group.MapPost("", CreateOrderEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Create an order"))
            .RequireAuthorization();
        group.MapPut("confirm-paid/{orderId}", ConfirmPaidOrderEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Confirm an order as paid"));
        group.MapPut("cancel/{orderId}", CancelOrderEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Cancel an order"))
            .RequireAuthorization();
        return group;
    }
}