using Domain.Models.Shared;

namespace Domain.Models;
public class Category : BaseEntity
{
    public required string Name { get; set; }
}