using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Shared;

namespace Domain.Models;
public class TicketType : BaseEntity
{
    public required Guid ShowId { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public required DateTimeOffset FromDate { get; set; }
    public required DateTimeOffset ToDate { get; set; }
    public int Amount { get; set; } = 0;
    public int LeastAmountBuy { get; set; } = 0;
    public int MostAmountBuy { get; set; } = 0;
    // -----------------------------------------
    [ForeignKey(nameof(ShowId))]
    public Show Show { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = [];
    public ICollection<Ticket> Tickets { get; set; } = [];
}