using API.Endpoints.EndpointHandler.OrderEndpointHandler.Commands;
using API.Endpoints.EndpointHandler.OrderEndpointHandler.Queries;
using Ardalis.Result;
using Domain.Enums;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Ticket;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Net.payOS.Types;
using Swashbuckle.AspNetCore.Annotations;
using UseCases.UC_Order.Commands.ConfirmPaidOrder;
using IResult = Microsoft.AspNetCore.Http.IResult;

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
        
        group.MapPost("confirm-webhook", ConfirmWebhookHandler)
            .WithMetadata(new SwaggerOperationAttribute("Confirm webhook"));
        
        group.MapPut("confirm-paid/{orderId}", ConfirmPaidOrderEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Confirm an order as paid"))
            .RequireAuthorization();
        group.MapPut("cancel/{orderId}", CancelOrderEndpointHandler.Handle)
            .WithMetadata(new SwaggerOperationAttribute("Cancel an order"))
            .RequireAuthorization();
        return group;
    }
    private static async Task<IResult> PayOsTransferHandler(PayOS payOs, [FromServices] ISender sender, [FromBody] WebhookType body, [FromServices] IUnitOfWork uow)
    {

        WebhookData data = payOs.verifyPaymentWebhookData(body);
        if (data.description == "Ma giao dich thu nghiem" || data.accountNumber == "VQRIO123")
        {
            return Results.Ok();
        }

        if (!body.success)
        {
            return Results.Ok();
        }

        var order = await uow.OrderRepository.FindAsync(x => x.Code == data.orderCode, true);
        if (order is null)
        {
            return Results.Ok();
        }
        Result<CreateTicketResponse> result = await sender.Send(new ConfirmPaidOrderCommand(order.Id));

        if (!result.IsSuccess) return Results.Ok();
        
        order.Status = OrderStatusEnum.Paid;
        
        await uow.SaveChangesAsync();
        
        return Results.Ok(data);
    }
    public record ConfirmWebhook(
        string webhook_url
    );
    private static async Task<IResult> ConfirmWebhookHandler(PayOS payOs, [FromBody] ConfirmWebhook body)
    { 
        var e = await payOs.confirmWebhook(body.webhook_url);
        Console.WriteLine(e);
        return Results.Ok();
    }
}