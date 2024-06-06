using Domain.Models.Shared;
namespace Domain.Models;
public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public ICollection<Order> Orders { get; set; } = [];
}