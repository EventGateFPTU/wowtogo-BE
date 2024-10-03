using Ardalis.Result;
using Net.payOS.Types;
using UseCases.Common.Models;

namespace UseCases.Common.Contracts;

public interface IPaymentService
{
    Task<Result<CreatePaymentResult>> CreatePaymentLink(string productName, string description, int price);
}