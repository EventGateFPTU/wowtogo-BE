using Domain.Models;
using Domain.Responses.Responses_Order;

namespace UseCases.Mapper.Mapper_Order
{
    public static class PendingOrderDBMapper
    {
        public static PendingOrderDB MapToPendingOrderDB(this Order order) => new PendingOrderDB(order.Id, order.TotalPrice, order.Currency, order.CreatedAt);
    }
}
