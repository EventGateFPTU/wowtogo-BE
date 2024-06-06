using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Shared;
namespace Domain.Models;
public class OrderItem : BaseEntity
{
    public int Quantity { get; set; } = 0;
    public float UnitPrice { get; set; } = 0;
    public float Price { get; set; } = 0;
    public Guid OrderId { get; set; }
    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; } = null!;
}