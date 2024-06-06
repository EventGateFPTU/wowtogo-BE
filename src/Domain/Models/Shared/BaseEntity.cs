namespace Domain.Models.Shared;
public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}