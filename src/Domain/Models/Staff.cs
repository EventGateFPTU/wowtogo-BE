using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Shared;

namespace Domain.Models;
public class Staff : BaseEntity
{
    public required Guid UserId { get; set; }
    public required Guid EventId { get; set; }
    //-----------------------------------------
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    [ForeignKey(nameof(EventId))]
    public Event Event { get; set; } = null!;
}