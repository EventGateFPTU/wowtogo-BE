using Ardalis.Result;
using Microsoft.Extensions.Configuration;
using Net.payOS;
using Net.payOS.Types;
using UseCases.Common.Contracts;

namespace Infrastructure.Services;

public class PaymentService(IConfiguration configuration, PayOS payOs) : IPaymentService
{
    public async Task<Result<CreatePaymentResult>> CreatePaymentLink(string productName, string description, int price)
    {
        int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
        ItemData item = new ItemData(productName, 1, price);
        List<ItemData> items = [item];
        var returnUrl = $"{configuration["CLIENT_ORIGIN_URL"]}/after-checkout";
        var cancelUrl = $"{configuration["CLIENT_ORIGIN_URL"]}/canceled";
        PaymentData paymentData = new PaymentData(orderCode, price, "vé", items, cancelUrl, returnUrl);
        
        CreatePaymentResult createPayment = await payOs.createPaymentLink(paymentData);
        return createPayment;
    }
}