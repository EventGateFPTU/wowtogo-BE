using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Shared;

namespace Domain.Models;
public class Show : BaseEntity
{
    public required Guid EventId { get; set; }
    public required string Title { get; set; }
    public required DateTimeOffset StartsAt { get; set; }
    public required DateTimeOffset EndsAt { get; set; }
    //----------------------------------------- 
    [ForeignKey(nameof(EventId))]
    public Event Event { get; set; } = null!;
    public ICollection<TicketType> TicketTypes { get; set; } = [];
}