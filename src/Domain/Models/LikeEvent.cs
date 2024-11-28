using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Shared;

namespace Domain.Models;

public class LikeEvent : BaseEntity
{
    public required Guid EventId { get; set; }
    public required Guid UserId { get; set; }
    
    [ForeignKey(nameof(EventId))]
    public Event Event { get; set; } = null!;
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
}