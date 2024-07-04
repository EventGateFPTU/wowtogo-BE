using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Shared;

namespace Domain.Models;
public class TicketTypeShow : BaseEntity
{
    public required Guid ShowId { get; set; }
    public required Guid TicketTypeId { get; set; }
    [ForeignKey(nameof(ShowId))]
    public Show Show { get; set; } = null!;
    [ForeignKey(nameof(TicketTypeId))]
    public TicketType TicketType { get; set; } = null!;
}