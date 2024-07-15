using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
using Domain.Models.Shared;

namespace Domain.Models;

public class Checkin : BaseEntity
{
    public required Guid ShowId { get; set; }
    public required Guid TicketId { get; set; }
    public UsedInFormatEnum UsedInFormat { get; set; } = UsedInFormatEnum.Code;
    public DateTimeOffset UsedAt { get; set; }
    //-----------------------------------------
    [ForeignKey(nameof(ShowId))]
    public Show Show { get; set; } = null!;
    [ForeignKey(nameof(TicketId))]
    public Ticket Ticket { get; set; } = null!;

    public void CheckIn(UsedInFormatEnum usedInFormat)
    {
        UsedAt = DateTimeOffset.UtcNow;
        UsedInFormat = usedInFormat;
    }
}