using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Shared;

namespace Domain.Models;
public class Show : BaseEntity
{
    public required Guid EventId { get; set; }
    public required DateTime StartsAt { get; set; }
    public required DateTime EndsAt { get; set; }
    //----------------------------------------- 
    [ForeignKey(nameof(EventId))]
    public Event Event { get; set; } = null!;
}