using Domain.Models.Shared;
namespace Domain.Models;
public class User : UserProfile
{
    public required string Subject
    {
        get;
        set;
    }
    // public string Password { get; set; } = string.Empty;
    //----------------------------------------- 
    // 
    // public Attendee Attendee { get; set; } = null!;
    // public Organizer Organizer { get; set; } = null!;
    // public Staff Staff { get; set; } = null!;
    // public ICollection<Order> Order { get; set; } = [];
}