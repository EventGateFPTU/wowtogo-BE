using Domain.Models;
using Domain.Responses.Responses_Order;

namespace UseCases.Mapper.Mapper_Order
{
    public static class PaidOrderDBMapper
    {
        public static PaidOrderDB MapToPaidOrderDB(this Order order) => new PaidOrderDB(order.Id, order.TotalPrice, order.Currency, order.CreatedAt);
    }
}
