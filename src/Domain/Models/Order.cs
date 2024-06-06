using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Shared;
namespace Domain.Models;
public class Order : BaseEntity
{
    public float TotalPrice { get; set; } = 0;
    public string Currency { get; set; } = string.Empty;
    public bool TicketsIssued { get; set; } = false;
    public required Guid CustomerId { get; set; }
    [ForeignKey(nameof(CustomerId))]
    public User Customer { get; set; } = null!;
}