using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
using Domain.Models.Shared;
namespace Domain.Models;
public class Ticket : BaseEntity
{
    public required Guid AttendeeId { get; set; }
    public required Guid TicketTypeId { get; set; }
    public string Code { get; set; } = string.Empty;
    public UsedInFormatEnum? UsedInFormat { get; set; } = UsedInFormatEnum.Code;
    public DateTimeOffset? UsedAt { get; set; } = DateTimeOffset.UtcNow;
    //----------------------------------------- 
    [ForeignKey(nameof(AttendeeId))]
    public Attendee Attendee { get; set; } = null!;
    [ForeignKey(nameof(TicketTypeId))]
    public TicketType TicketType { get; set; } = null!;
}