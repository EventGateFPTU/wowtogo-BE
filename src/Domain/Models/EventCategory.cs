using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Shared;

namespace Domain.Models;
public class EventCategory : BaseEntity
{
    public required Guid CategoryId { get; set; }
    public required Guid EventId { get; set; }
    //-----------------------------------------
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = null!;
    [ForeignKey(nameof(EventId))]
    public Event Event { get; set; } = null!;

}