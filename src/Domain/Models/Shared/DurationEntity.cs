namespace Domain.Models.Shared;
public class DurationEntity : BaseEntity
{
    public DateTime StartsAt { get; set; } = DateTime.UtcNow;
    public DateTime EndsAt { get; set; } = DateTime.UtcNow;
}