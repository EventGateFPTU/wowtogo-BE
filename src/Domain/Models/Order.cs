using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
using Domain.Models.Shared;
namespace Domain.Models;
public class Order : BaseEntity
{
    public decimal TotalPrice { get; set; } = 0;
    public OrderStatusEnum Status { get; set; } = OrderStatusEnum.Pending;
    public string Currency { get; set; } = string.Empty;
    public required Guid TicketTypeId { get; set; }
    public required Guid UserId { get; set; }
    //----------------------------------------- 
    [ForeignKey(nameof(TicketTypeId))]
    public TicketType TicketType { get; set; } = null!;
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    public void AcceptOrder()
    {
        Status = OrderStatusEnum.Paid;
    }
    public void CancelOrder()
    {
        Status = OrderStatusEnum.Canceled;
    }
}