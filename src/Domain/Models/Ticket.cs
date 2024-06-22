using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
using Domain.Models.Shared;
namespace Domain.Models;
public class Ticket : BaseEntity
{
    public required Guid AttendeeId { get; set; }
    public required Guid TicketTypeId { get; set; }
    public string Code { get; set; } = string.Empty;
    public UsedInFormatEnum? UsedInFormat { get; set; } = null!;
    public DateTimeOffset? UsedAt { get; set; } = null!;
    //----------------------------------------- 
    [ForeignKey(nameof(AttendeeId))]
    public Attendee Attendee { get; set; } = null!;
    [ForeignKey(nameof(TicketTypeId))]
    public TicketType TicketType { get; set; } = null!;

    public bool IsCheckedIn()
    {
        return UsedAt.HasValue && UsedInFormat.HasValue;
    }
    public void CheckIn(UsedInFormatEnum usedInFormat)
    {
        UsedAt = DateTimeOffset.UtcNow;
        UsedInFormat = usedInFormat;
    }
}