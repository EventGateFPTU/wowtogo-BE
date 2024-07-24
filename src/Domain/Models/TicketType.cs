using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Shared;

namespace Domain.Models;
public class TicketType : BaseEntity
{
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public required DateTimeOffset FromDate { get; set; }
    public required DateTimeOffset ToDate { get; set; }
    public int Amount { get; set; } = 1;
    public int LeastAmountBuy { get; set; } = 1;
    public int MostAmountBuy { get; set; } = 1;
    public required Guid EventId { get; set; }
    // -----------------------------------------
    [ForeignKey(nameof(EventId))]
    public Event Event { get; set; } = null!;
    public ICollection<TicketTypeShow> TicketTypeShows { get; set; } = [];
    public ICollection<Order> Orders { get; set; } = [];
    public ICollection<Ticket> Tickets { get; set; } = [];

    public (bool result, string message) IsValid()
    {
        // date condition
        if (FromDate > ToDate) return (false, "From Date must be sooner than the To Date");
        if (FromDate < DateTimeOffset.UtcNow) return (false, "From Date must be in the past");
        // amount condition
        if (Amount < 0) return (false, "Amount must be a positive number");
        if (LeastAmountBuy < 0) return (false, "Least Amount Buy must be a positive number");
        if (MostAmountBuy < 0) return (false, "Most Amount Buy must be a positive number");
        if (Price < 0) return (false, "Price must be a positive number");
        if (LeastAmountBuy > MostAmountBuy) return (false, "Least Amount Buy must be lower then Most Amount Buy");
        if (Amount < LeastAmountBuy) return (false, "Amount must greater than Least Amount Buy");
        if (Amount < MostAmountBuy) return (false, "Amount must greater than Most Amount Buy");
        return (true, "valid");
    }
}