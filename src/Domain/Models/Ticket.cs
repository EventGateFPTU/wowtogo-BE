using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Shared;
namespace Domain.Models;
public class Ticket : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public DateTime UsedAt { get; set; } = DateTime.UtcNow;
    public required Guid EventId { get; set; }
    public required Guid OrderId { get; set; }
    [ForeignKey(nameof(EventId))]
    public Event Event { get; set; } = null!;
    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; } = null!;
}