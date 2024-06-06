using Domain.Enums;
using Domain.Models.Shared;

namespace Domain.Models;
public class Event : DurationEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int Status { get; set; } = (int)EventStatusEnum.Draft;
    public ICollection<Ticket> Tickets { get; set; } = [];
}