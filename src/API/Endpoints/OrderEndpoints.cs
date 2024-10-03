using API.Endpoints.EndpointHandler.OrderEndpointHandler.Commands;
using API.Endpoints.EndpointHandler.OrderEndpointHandler.Queries;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Net.payOS.Types;
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
        
        group.MapPost("payos_transfer_handler", PayOsTransferHandler)
            .WithMetadata(new SwaggerOperationAttribute("Confirm webhook"));
        
        group.MapPut("confirm-paid/{orderId}", ConfirmPaidOrderEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Confirm an order as paid"))
            .RequireAuthorization();
        group.MapPut("cancel/{orderId}", CancelOrderEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Cancel an order"))
            .RequireAuthorization();
        return group;
    }

    private static IResult PayOsTransferHandler(PayOS payOs, [FromBody] WebhookType body)
    {
        WebhookData data = payOs.verifyPaymentWebhookData(body);

        return Results.Ok(data);
    }
}