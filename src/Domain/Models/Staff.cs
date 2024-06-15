using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Shared;

namespace Domain.Models;
public class Staff : BaseEntity
{
    public Guid EventId { get; set; }
    //-----------------------------------------
    [ForeignKey(nameof(Id))]
    public User User { get; set; } = null!;
    [ForeignKey(nameof(EventId))]
    public Event Event { get; set; } = null!;
}