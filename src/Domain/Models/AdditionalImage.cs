using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Shared;

namespace Domain.Models;

public class AdditionalImage : BaseEntity
{
    public required Guid EventId { get; set; }
    public required string Url { get; set; }
    public int SlotNumber { get; set; } = 1;

    [ForeignKey(nameof(EventId))]
    public Event Event { get; set; } = null!;
}